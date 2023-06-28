// DO NOT EDIT MANUALLY
// Generated Tuesday, 27 June 2023 19:30:10
// DO NOT EDIT MANUALLY
using DialogueScript;

namespace TestNamespace
{
    public struct TestClass : Script
    {
        /* test */
        /* test */
        // Flag Map: ID - Name
        // 0 - flag1
        // 1 - flag2
        // 2 - a1
        public new static int FlagCount() => 3;
        public new static int ScriptId() => 0;
        public new static string ScriptName() => "TestClass";
        public void Tick(ExecutionContext context)
        {
            do
            {
                // Reset flag set alarm
                context.ResetFlagSetAlarm();
                // Scheduled Block - 0
                if (context.IsBlockExecuted(0) && context.IsFlagSet(0) && context.IsFlagSet(1))
                {
                    Block0(context);
                }

                // Scheduled Block - 1
                if (context.IsBlockExecuted(1) && context.IsFlagSet(2))
                {
                    Block1(context);
                }
            }
            while (context.IsFlagSetAlarmTriggered());
        }

        private void Block0(ExecutionContext context)
        {
        }

        private void Block1(ExecutionContext context)
        {
        }
    }
}