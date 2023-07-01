// DO NOT EDIT MANUALLY
// Generated Saturday, 01 July 2023 13:01:49
// DO NOT EDIT MANUALLY
namespace DialogueScript
{
    public static partial class Functions
    {
        public struct TestClass : Script
        {
            public static int ScriptId() => 0;
            public static string ScriptName() => "TestScript";
            public void Tick(ExecutionContext context)
            {
                do
                {
                    // Reset flag set alarm
                    context.ResetFlagSetAlarm();
                    // Scheduled Block - 0
                    if (!context.IsBlockExecuted(0) && context.IsFlagSet(Flag.flag1) && context.IsFlagSet(Flag.flag2))
                    {
                        Block0(context);
                    }

                    // Scheduled Block - 1
                    if (!context.IsBlockExecuted(1) && context.IsFlagSet(Flag.a1))
                    {
                        Block1(context);
                    }
                }
                while (context.IsFlagSetAlarmTriggered());
            }

            private void Block0(ExecutionContext context)
            {
                Best();
            }

            private void Block1(ExecutionContext context)
            {
                Test();
            }
        }
    }
}