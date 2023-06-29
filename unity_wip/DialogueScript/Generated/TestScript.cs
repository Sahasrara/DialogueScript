// DO NOT EDIT MANUALLY
// Generated Wednesday, 28 June 2023 20:19:22
// DO NOT EDIT MANUALLY
using DialogueScript;

namespace TestNamespace
{
    public struct TestClass : Script
    {
        // Flag Map: ID - Name
        // 0 - flag1
        // 1 - flag2
        // 2 - a1
        public static int FlagCount() => 3;
        public static int ScriptId() => 0;
        public static string ScriptName() => "TestScript";
        public void Tick(ExecutionContext context)
        {
            do
            {
                // Reset flag set alarm
                context.ResetFlagSetAlarm();
                // Scheduled Block - 0
                if (!context.IsBlockExecuted(0) && context.IsFlagSet(0) && context.IsFlagSet(1))
                {
                    Block0(context);
                }

                // Scheduled Block - 1
                if (!context.IsBlockExecuted(1) && context.IsFlagSet(2))
                {
                    Block1(context);
                }
            }
            while (context.IsFlagSetAlarmTriggered());
        }

        private void Block0(ExecutionContext context)
        {
            TestNamespace.TestScriptHelpers.Best();
        }

        private void Block1(ExecutionContext context)
        {
            TestNamespace.TestScriptHelpers.Test();
        }
    }
}