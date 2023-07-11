using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TranspilingTreeWalker
    {
        #region Constants
        private const string k_BlockNamePrefix = "Block";
        private const string k_FunctionsClassName = "Functions"; // TODO - make this configurable
        #endregion

        #region Private Variables
        private int m_ScriptId;
        private string m_ClassName;
        private string m_ScriptName;

        // Accumulator - Script
        private StringBuilder m_AccumulatorScript;

        private FlagCache m_FlagCache;
        private List<ScheduledBlockBuilder> m_ScheduledBlocks;
        #endregion

        public static string WalkScript(DialogueScriptParser.ScriptContext scriptTree, FlagCache flagCache,
            string className, string scriptName, int scriptId)
        {
            TranspilingTreeWalker walker = new(flagCache, className, scriptName, scriptId);
            walker.Walk(scriptTree);
            return walker.ToString();
        }

        private TranspilingTreeWalker(FlagCache flagCache,  string className, string scriptName,
            int scriptId)
        {
            // Setup Accumulators
            m_AccumulatorScript = new();

            // Set Flag Cache
            m_FlagCache = flagCache;

            // Setup Schedule Block Lost
            m_ScheduledBlocks = new();

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
                    if (terminalNode.Symbol.Type != DialogueScriptParser.Eof)
                    {
                        m_ScheduledBlocks[^1].Code.Append(terminalNode.Symbol.Text);
                    }
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
                // case DialogueScriptParser.Expression_postfix_invokeContext invokeContext:
                //     HandleInvoke(invokeContext);
                //     break;
                case DialogueScriptParser.Expression_postfix_invoke_asyncContext invokeAsyncContext:
                    HandleInvokeAsync(invokeAsyncContext);
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
            m_AccumulatorScript.AppendLine(Helpers.GetGeneratedCodeHeader());
            m_AccumulatorScript.AppendLine("namespace DialogueScript");
            m_AccumulatorScript.AppendLine("{"); // namespace - OPEN
            m_AccumulatorScript.AppendLine($"public static partial class {k_FunctionsClassName}");
            m_AccumulatorScript.AppendLine("{"); // functions class - OPEN
            m_AccumulatorScript.AppendLine($"public struct {m_ClassName} : {nameof(IScript)}");
            m_AccumulatorScript.AppendLine("{"); // script - OPEN

            // Write Script Body
            int childCount = scriptContext.ChildCount;
            for (int i = 0; i < childCount; ++i)
            {
                Walk(scriptContext.GetChild(i));
            }

            // ScriptId()
            m_AccumulatorScript.AppendLine($"public static int ScriptId() => {m_ScriptId};");

            // ScriptName()
            m_AccumulatorScript.AppendLine($"public static string ScriptName() => \"{m_ScriptName}\";");

            // Create Execution Context Generator
            m_AccumulatorScript.AppendLine("public ExecutionContext CreateExecutionContext()");
            m_AccumulatorScript.AppendLine("{");
            m_AccumulatorScript.AppendLine($"int blockCount = {m_ScheduledBlocks.Count};");
            m_AccumulatorScript.AppendLine("ExecutionContext.BlockData[] blockData = new ExecutionContext.BlockData[blockCount];");
            bool noAsync = true;
            for (int i = 0; i < m_ScheduledBlocks.Count; i++)
            {
                ScheduledBlockBuilder scheduledBlock = m_ScheduledBlocks[i];
                m_AccumulatorScript.AppendLine($"blockData[{i}] = new ExecutionContext.BlockData();");
                m_AccumulatorScript.AppendLine($"blockData[{i}].AsyncFunctionCompleteArray = new bool[{scheduledBlock.AsyncFunctionCount}];");
                m_AccumulatorScript.AppendLine($"blockData[{i}].TriggerBlockExitFlags = Block{i}ExitFlags;");
                if (scheduledBlock.AsyncFunctionCount == 0)
                {
                    m_AccumulatorScript.AppendLine($"blockData[{i}].AsyncDone = true;");
                }
                else noAsync = false;
            }
            if (noAsync)
            {
                m_AccumulatorScript.AppendLine("return new(new bool[(int)Flag.RESERVED_FLAG_COUNT], blockData, false);");
            }
            else
            {
                m_AccumulatorScript.AppendLine("return new(new bool[(int)Flag.RESERVED_FLAG_COUNT], blockData, true);");
            }
            m_AccumulatorScript.AppendLine("}");

            // Tick() - open
            m_AccumulatorScript.AppendLine("public void Tick(ExecutionContext context)");
            m_AccumulatorScript.AppendLine("{");

            // Tick() do-while loop - open
            m_AccumulatorScript.AppendLine("if (context.IsSynchronousCodeExecuted()) return;");
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
                foreach (string entryFlag in scheduledBlock.EntryFlags)
                {
                    m_AccumulatorScript.Append($" && context.IsFlagSet((int)Flag.{entryFlag})");
                }

                m_AccumulatorScript.AppendLine(")");
                m_AccumulatorScript.AppendLine("{");
                // Execute Block
                m_AccumulatorScript.AppendLine($"{k_BlockNamePrefix}{i}(context);");
                m_AccumulatorScript.AppendLine("}");
            }

            // Tick() do-while loop - close
            m_AccumulatorScript.AppendLine("} while(!context.IsSynchronousCodeExecuted() && context.IsFlagSetAlarmTriggered());");

            // Tick() - close
            m_AccumulatorScript.AppendLine("}");

            // Generate Scheduled Block Functions
            for (int i = 0; i < m_ScheduledBlocks.Count; i++)
            {
                ScheduledBlockBuilder builder = m_ScheduledBlocks[i];
                m_AccumulatorScript.AppendLine($"private void {builder.GetMethodName()}(ExecutionContext context)");
                m_AccumulatorScript.AppendLine("{");
                m_AccumulatorScript.AppendLine(builder.Code.ToString());

                // Mark scheduled block executed
                m_AccumulatorScript.AppendLine("// Mark schedule block executed");
                m_AccumulatorScript.AppendLine($"context.SetBlockExecuted({i});");
                m_AccumulatorScript.AppendLine("}");

                // Trigger Exit Flag Function
                m_AccumulatorScript.AppendLine($"private void {builder.GetMethodName()}ExitFlags(ExecutionContext context)");
                m_AccumulatorScript.AppendLine("{");
                if (builder.ExitFlags.Count > 0)
                {
                    foreach (string exitFlag in builder.ExitFlags)
                    {
                        m_AccumulatorScript.AppendLine($"context.SetFlag((int)Flag.{exitFlag});");
                    }
                }
                m_AccumulatorScript.AppendLine("}");
            }

            // Close Contexts
            m_AccumulatorScript.AppendLine("}"); // script - CLOSE
            m_AccumulatorScript.AppendLine("}"); // functions class - CLOSE
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

                    // Add flag to flag cache
                    m_FlagCache.AddFlag(flag);

                    // Add flag to scheduled block builder (idempotent)
                    if (isEntry) builder.EntryFlags.Add(flag);
                    else builder.ExitFlags.Add(flag);
                }
            }
        }
        #endregion

        // #region Namespace Node
        // private void HandleNamespaceNode(DialogueScriptParser.NamespaceContext namespaceContext)
        // {
        //     int childCount = namespaceContext.ChildCount;
        //     for (int i = 0; i < childCount; ++i)
        //     {
        //         IParseTree child = namespaceContext.GetChild(i);
        //         if (child is ITerminalNode terminalNode
        //             && terminalNode.Symbol.Type == DialogueScriptParser.COLONCOLON)
        //         {
        //             // Replace '::' with '.' for C#
        //             m_ScheduledBlocks[^1].BlockCode.Append('.');
        //         }
        //         else Walk(child);
        //     }
        // }
        // #endregion

        #region Invoke
        // private void HandleInvoke(DialogueScriptParser.Expression_postfix_invokeContext invokeContext)
        // {
        //     int childCount = invokeContext.ChildCount;
        //     for (int i = 0; i < childCount; ++i)
        //     {
        //         IParseTree child = invokeContext.GetChild(i);
        //         m_ScheduledBlocks[^1].Code.Append($"{k_FunctionsClassName}.");
        //
        //         if (child is ITerminalNode terminalNode
        //             && terminalNode.Symbol.Type == DialogueScriptParser.COLONCOLON)
        //         {
        //             // Replace '::' with '.' for C#
        //             m_ScheduledBlocks[^1].Code.Append('.');
        //         }
        //         else Walk(child);
        //     }
        // }
        #endregion

        #region Invoke Async
        private void HandleInvokeAsync(DialogueScriptParser.Expression_postfix_invoke_asyncContext invokeAsyncContext)
        {
            int argumentCount = invokeAsyncContext.expression_list() == null
                ? 0
                : invokeAsyncContext.expression_list().ChildCount;
            int childCount = invokeAsyncContext.ChildCount;
            for (int i = 0; i < childCount; i++)
            {
                // Add the async parameter after
                IParseTree child = invokeAsyncContext.GetChild(i);
                if (child is ITerminalNode lBraceNode && lBraceNode.Symbol.Type == DialogueScriptParser.LBRACE)
                {
                    ScheduledBlockBuilder builder = m_ScheduledBlocks[^1];

                    // Add in AsyncFunctionSignalComplete as first parameter
                    m_ScheduledBlocks[^1].Code.Append($"(context.CreateAsyncFunctionCompleteSignal({builder.ScheduledBlockID}, {builder.GetThenIncrementAsyncFunctionCount()})");
                    if (argumentCount > 0)
                    {
                        m_ScheduledBlocks[^1].Code.Append(",");
                    }
                }
                else if (child is ITerminalNode rBraceNode && rBraceNode.Symbol.Type == DialogueScriptParser.RBRACE)
                {
                    // Replace curly bracket with parenthesis
                    m_ScheduledBlocks[^1].Code.Append(")");
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

        #region Helpers - Scheduled Blocks
        private class ScheduledBlockBuilder
        {
            public int AsyncFunctionCount { get; private set; }
            public int ScheduledBlockID { get; set; }
            public HashSet<string> ExitFlags { get; }
            public HashSet<string> EntryFlags { get; }
            public StringBuilder Code { get; }

            public string GetMethodName() => $"Block{ScheduledBlockID}";
            public int GetThenIncrementAsyncFunctionCount()
            {
                int count = AsyncFunctionCount;
                AsyncFunctionCount++;
                return count;
            }

            public ScheduledBlockBuilder()
            {
                ExitFlags = new();
                EntryFlags = new();
                Code = new();
            }
        }
        #endregion
    }
}