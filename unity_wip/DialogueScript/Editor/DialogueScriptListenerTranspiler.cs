using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime.Tree;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DialogueScript
{
    public class DialogueScriptListenerTranspiler : DialogueScriptParserBaseListener
    {
        #region Private Variables
        private string m_Namespace;
        private string m_ClassName;

        // Accumulator - Script
        private StringBuilder m_AccumulatorScript;
        // Accumulator - Scheduled Block
        private StringBuilder m_AccumulatorScheduledBlock;

        private List<string> m_FlagList;
        private Dictionary<string, int> m_FlagMap;
        private List<ScheduledBlockBuilder> m_ScheduledBlocks;
        #endregion

        #region Constructor
        public DialogueScriptListenerTranspiler(string namespaceString, string className)
        {
            // Setup Accumulators
            m_AccumulatorScript = new();
            m_AccumulatorScheduledBlock = new();

            // Setup Global Flag Containers
            m_FlagMap = new();
            m_FlagList = new();

            // Setup Schedule Block Lost
            m_ScheduledBlocks = new();

            // Write Warning Header
            m_AccumulatorScript.AppendLine("// DO NOT EDIT MANUALLY");
            m_AccumulatorScript.AppendLine("// Generated " + System.DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
            m_AccumulatorScript.AppendLine("// DO NOT EDIT MANUALLY");
            m_AccumulatorScript.AppendLine();

            // Namespace
            m_Namespace = namespaceString;

            // Classname
            m_ClassName = className;
        }
        #endregion

        #region ToString
        public override string ToString()
            => CSharpSyntaxTree.ParseText(m_AccumulatorScript.ToString())
                .GetRoot()
                .NormalizeWhitespace()
                .SyntaxTree
                .GetText()
                .ToString();
        #endregion

        #region Visitor Methods - Script
        public override void EnterScript(DialogueScriptParser.ScriptContext context)
        {
            m_AccumulatorScript.AppendLine($"namespace {m_Namespace}");
            m_AccumulatorScript.AppendLine("{"); // NAMESPACE OPEN
            m_AccumulatorScript.AppendLine($"public static class {m_ClassName}");
            m_AccumulatorScript.AppendLine("{"); // CLASS OPEN
        }

        public override void ExitScript(DialogueScriptParser.ScriptContext context)
        {
            // Generate Tick Function
            m_AccumulatorScript.AppendLine("public static void Tick()");
            m_AccumulatorScript.AppendLine("{");
            // TODO - check each block for the following:
            // - not already executed
            // - entry flags all set true
            //
            // Inside the if statement, execute the scheduled block code.
            // EX:
            // if (!executionContext.IsExecuted(i) && executionContext.FlagsSet(i))
            // {
            //     ScheduledBlock1(executionContext);
            // }
            m_AccumulatorScript.AppendLine("}");

            // Generate Scheduled Block Functions
            for (int i = 0; i < m_ScheduledBlocks.Count; i++)
            {
                m_AccumulatorScript.AppendLine($"private static void Block{i}()");
                m_AccumulatorScript.AppendLine("{");
                // TODO
                m_AccumulatorScript.AppendLine("}");
            }

            // Close Contexts
            m_AccumulatorScript.AppendLine("}"); // CLASS CLOSE
            m_AccumulatorScript.AppendLine("}"); // NAMESPACE CLOSE
        }
        #endregion

        #region Visitor Methods - Scheduled Block
        public override void ExitScheduled_block_open(DialogueScriptParser.Scheduled_block_openContext context)
        {
            // Add Schedule Block Builder
            ScheduledBlockBuilder scheduledBlockBuilder = new()
            {
                ScheduledBlockID = m_ScheduledBlocks.Count,
            };
            m_ScheduledBlocks.Add(scheduledBlockBuilder);

            // Add flags to block
            AddFlagListToBlock(context.flag_list(), scheduledBlockBuilder, false);
        }

        public override void ExitScheduled_block_close(DialogueScriptParser.Scheduled_block_closeContext context)
        {
            // Add flags to block
            AddFlagListToBlock(context.flag_list(), m_ScheduledBlocks[^1], false);
        }

        private void AddFlagListToBlock(
            DialogueScriptParser.Flag_listContext flagList, ScheduledBlockBuilder builder, bool isEntry)
        {
            if (flagList.ChildCount > 0)
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

        #region Visitor Methods - Function Invocation
        public override void EnterExpression_postfix_invoke(
            DialogueScriptParser.Expression_postfix_invokeContext context)
        {
            m_AccumulatorScript.AppendLine("/* test */");
        }
        #endregion

        #region Helpers - Scheduled Blocks
        private class ScheduledBlockBuilder
        {
            public int ScheduledBlockID { get; set; }
            public HashSet<int> ExitFlags { get; private set; }
            public HashSet<int> EntryFlags { get; private set; }

            public ScheduledBlockBuilder()
            {
                ExitFlags = new();
                EntryFlags = new();
            }
        }
        #endregion

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
    }
}
