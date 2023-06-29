using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime.Tree;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DialogueScript
{
    // TODO's
    // - check if any flags are never triggered
    // - better transpiler error reporting (something better than the "bail" handler)
    // - make it so invocations without prefix default to a specific static class
    public class DialogueScriptTranspilingTreeWalker
    {
        #region Constants
        private const string k_BlockNamePrefix = "Block";
        #endregion

        #region Private Variables
        private int m_ScriptId;
        private string m_Namespace;
        private string m_ClassName;
        private string m_ScriptName;
        private bool m_ShouldCaptureTerminalNodesToBlockBuffer;

        // Accumulator - Script
        private StringBuilder m_AccumulatorScript;

        private List<string> m_FlagList;
        private Dictionary<string, int> m_FlagMap;
        private List<ScheduledBlockBuilder> m_ScheduledBlocks;
        #endregion

        public static string WalkScript(DialogueScriptParser.ScriptContext scriptTree, string namespaceString,
            string className, string scriptName, int scriptId)
        {
            DialogueScriptTranspilingTreeWalker walker = new(namespaceString, className, scriptName, scriptId);
            walker.Walk(scriptTree);
            return walker.ToString();
        }

        private DialogueScriptTranspilingTreeWalker(string namespaceString, string className, string scriptName, int scriptId)
        {
            // Setup Accumulators
            m_AccumulatorScript = new();

            // Setup Global Flag Containers
            m_FlagMap = new();
            m_FlagList = new();

            // Setup Schedule Block Lost
            m_ScheduledBlocks = new();

            // Namespace
            m_Namespace = namespaceString;

            // Class name
            m_ClassName = className;

            // Script name
            m_ScriptName = scriptName;

            // Script Id
            m_ScriptId = scriptId;
        }

        #region ToString
        public override string ToString()
            => CSharpSyntaxTree.ParseText(m_AccumulatorScript.ToString())
                .GetRoot()
                .NormalizeWhitespace()
                .SyntaxTree
                .GetText()
                .ToString();
        #endregion

        private void Walk(IParseTree t)
        {
            switch (t)
            {
                case IErrorNode errorNode:
                    throw new Exception(errorNode.ToString());
                case ITerminalNode terminalNode:
                    m_ScheduledBlocks[^1].BlockCode.Append(terminalNode.Symbol.Text);
                    break;
                case IRuleNode ruleNode:
                    HandleRuleNode(ruleNode);
                    break;
                default:
                    throw new Exception($"Unknown node type: {t}");
            }
        }

        private void HandleRuleNode(IRuleNode ruleNode)
        {
            // EnterRule(listener, r);
            // int childCount = r.ChildCount;
            // for (int i = 0; i < childCount; ++i)
            //     Walk(listener, r.GetChild(i));
            // ExitRule(listener, r);
            switch (ruleNode)
            {
                case DialogueScriptParser.ScriptContext scriptContext:
                    HandleScript(scriptContext);
                    break;
                case DialogueScriptParser.Scheduled_block_openContext scheduledBlockOpenContext:
                    HandleScheduledBlockOpen(scheduledBlockOpenContext);
                    break;
                case DialogueScriptParser.Scheduled_block_closeContext scheduledBlockCloseContext:
                    HandleScheduledBlockClose(scheduledBlockCloseContext);
                    break;
                case DialogueScriptParser.Expression_postfix_invokeContext:
                    // TODO
                    break;
                case DialogueScriptParser.Expression_postfix_invoke_asyncContext:
                    // TODO
                    break;
                case DialogueScriptParser.NamespaceContext namespaceContext:
                    HandleNamespaceNode(namespaceContext);
                    break;
                default:
                    HandleNodeDefault(ruleNode);
                    break;
            }
        }

        #region Script
        private void HandleScript(DialogueScriptParser.ScriptContext scriptContext)
        {
            // Write File Header
            m_AccumulatorScript.AppendLine("// DO NOT EDIT MANUALLY");
            m_AccumulatorScript.AppendLine("// Generated " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
            m_AccumulatorScript.AppendLine("// DO NOT EDIT MANUALLY");
            m_AccumulatorScript.AppendLine();
            m_AccumulatorScript.AppendLine("using DialogueScript;");
            m_AccumulatorScript.AppendLine($"namespace {m_Namespace}");
            m_AccumulatorScript.AppendLine("{"); // namespace - open
            m_AccumulatorScript.AppendLine($"public struct {m_ClassName} : Script");
            m_AccumulatorScript.AppendLine("{"); // script - open

            // Write Script Body
            int childCount = scriptContext.ChildCount;
            for (int i = 0; i < childCount; ++i)
            {
                Walk(scriptContext.GetChild(i));
            }

            // Comment Flags Names
            m_AccumulatorScript.AppendLine("// Flag Map: ID - Name");
            for (int i = 0; i < m_FlagList.Count; i++)
            {
                m_AccumulatorScript.AppendLine($"// {i} - {m_FlagList[i]}");
            }

            // FlagCount()
            m_AccumulatorScript.AppendLine($"public static int FlagCount() => {m_FlagList.Count};");

            // ScriptId()
            m_AccumulatorScript.AppendLine($"public static int ScriptId() => {m_ScriptId};");

            // ScriptName()
            m_AccumulatorScript.AppendLine($"public static string ScriptName() => \"{m_ScriptName}\";");

            // Tick() - open
            m_AccumulatorScript.AppendLine("public void Tick(ExecutionContext context)");
            m_AccumulatorScript.AppendLine("{");

            // Tick() do-while loop - open
            m_AccumulatorScript.AppendLine("do {");
            m_AccumulatorScript.AppendLine("// Reset flag set alarm");
            m_AccumulatorScript.AppendLine("context.ResetFlagSetAlarm();");

            // Check Each Scheduled Block in the Tick
            for (int i = 0; i < m_ScheduledBlocks.Count; i++)
            {
                // Grab Scheduled Block
                ScheduledBlockBuilder scheduledBlock = m_ScheduledBlocks[i];

                // Scheduled Block Header
                m_AccumulatorScript.AppendLine($"// Scheduled Block - {i}");

                // Execution Condition Check
                m_AccumulatorScript.Append("if (");

                // Make sure block hasn't already executed
                m_AccumulatorScript.Append($"!context.IsBlockExecuted({i})");

                // Check if required flags are set
                foreach (int entryFlagId in scheduledBlock.EntryFlags)
                {
                    m_AccumulatorScript.Append($" && context.IsFlagSet({m_FlagMap[m_FlagList[entryFlagId]]})");
                }

                m_AccumulatorScript.AppendLine(")");
                m_AccumulatorScript.AppendLine("{");
                m_AccumulatorScript.AppendLine($"{k_BlockNamePrefix}{i}(context);");
                m_AccumulatorScript.AppendLine("}");
            }

            // Tick() do-while loop - close
            m_AccumulatorScript.AppendLine("} while(context.IsFlagSetAlarmTriggered());");

            // Tick() - close
            m_AccumulatorScript.AppendLine("}");

            // Generate Scheduled Block Functions
            for (int i = 0; i < m_ScheduledBlocks.Count; i++)
            {
                ScheduledBlockBuilder builder = m_ScheduledBlocks[i];
                m_AccumulatorScript.AppendLine($"private void {builder.GetMethodName()}(ExecutionContext context)");
                m_AccumulatorScript.AppendLine("{");
                m_AccumulatorScript.Append(builder.BlockCode.ToString());
                m_AccumulatorScript.AppendLine("}");
            }

            // Close Contexts
            m_AccumulatorScript.AppendLine("}"); // script - CLOSE
            m_AccumulatorScript.AppendLine("}"); // namespace - CLOSE
        }
        #endregion

        #region Scheduled Block
        private void HandleScheduledBlockOpen(
            DialogueScriptParser.Scheduled_block_openContext scheduledBlockOpenContext)
        {
            // Add Schedule Block Builder
            ScheduledBlockBuilder scheduledBlockBuilder = new()
            {
                ScheduledBlockID = m_ScheduledBlocks.Count,
            };
            m_ScheduledBlocks.Add(scheduledBlockBuilder);

            // Add flags to block
            AddFlagListToBlock(scheduledBlockOpenContext.flag_list(), scheduledBlockBuilder, true);
        }

        private void HandleScheduledBlockClose(
            DialogueScriptParser.Scheduled_block_closeContext scheduledBlockCloseContext)
        {
            // Add flags to block
            AddFlagListToBlock(scheduledBlockCloseContext.flag_list(), m_ScheduledBlocks[^1], false);
        }

        private void AddFlagListToBlock(
            DialogueScriptParser.Flag_listContext flagList, ScheduledBlockBuilder builder, bool isEntry)
        {
            if (flagList != null && flagList.ChildCount > 0)
            {
                for (int i = 0; i < flagList.ChildCount; i++)
                {
                    // Sanity
                    if (flagList.children[i] is not ITerminalNode identifier)
                    {
                        throw new Exception("Scheduled block flag list must be a comma separated list of identifiers");
                    }

                    // Grab flag name and skip commas
                    string flag = identifier.GetText();
                    if (flag == ",") continue;

                    // Add flag to global trackers
                    int flagId = AddFlag(flag);

                    // Add flag to scheduled block builder (idempotent)
                    if (isEntry) builder.EntryFlags.Add(flagId);
                    else builder.ExitFlags.Add(flagId);
                }
            }
        }
        #endregion

        #region Namespace Node
        private void HandleNamespaceNode(DialogueScriptParser.NamespaceContext namespaceContext)
        {
            int childCount = namespaceContext.ChildCount;
            for (int i = 0; i < childCount; ++i)
            {
                IParseTree child = namespaceContext.GetChild(i);
                if (child is ITerminalNode terminalNode
                    && terminalNode.Symbol.Type == DialogueScriptParser.COLONCOLON)
                {
                    // Replace '::' with '.' for C#
                    m_ScheduledBlocks[^1].BlockCode.Append('.');
                }
                else Walk(child);
            }
        }
        #endregion

        private void HandleNodeDefault(IRuleNode ruleNode)
        {
            int childCount = ruleNode.ChildCount;
            for (int i = 0; i < childCount; ++i)
            {
                Walk(ruleNode.GetChild(i));
            }
        }

        #region Helpers - Flags
        private int AddFlag(string flag)
        {
            // If flag is new, add it, otherwise return the previous flag ID
            if (!m_FlagMap.TryGetValue(flag, out var flagId))
            {
                flagId = m_FlagList.Count;
                m_FlagMap[flag] = flagId;
                m_FlagList.Add(flag);
            }
            return flagId;
        }
        private string GetFlagString(int flagId) => m_FlagList[flagId];
        private int GetFlagId(string flagString) => m_FlagMap[flagString];
        #endregion

        #region Helpers - Scheduled Blocks
        private class ScheduledBlockBuilder
        {
            public int ScheduledBlockID { get; set; }
            public HashSet<int> ExitFlags { get; private set; }
            public HashSet<int> EntryFlags { get; private set; }
            public StringBuilder BlockCode { get; private set; }

            public string GetMethodName() => $"Block{ScheduledBlockID}";

            public ScheduledBlockBuilder()
            {
                ExitFlags = new();
                EntryFlags = new();
                BlockCode = new();
            }
        }
        #endregion
    }
}


