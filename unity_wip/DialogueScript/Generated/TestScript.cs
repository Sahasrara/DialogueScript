// DO NOT EDIT MANUALLY
// Generated Wednesday, 05 July 2023 18:32:57
// DO NOT EDIT MANUALLY
namespace DialogueScript
{
    public static partial class Functions
    {
        public struct TestClass : IScript
        {
            public static int ScriptId() => 0;
            public static string ScriptName() => "TestScript";
            public int BlockCount() => 2;
            public void Tick(ExecutionContext context)
            {
                do
                {
                    // Reset flag set alarm
                    context.ResetFlagSetAlarm();
                    // Scheduled Block - 0
                    if (!context.IsBlockExecuted(0))
                    {
                        Block0(context);
                    }

                    // Scheduled Block - 1
                    if (!context.IsBlockExecuted(1) && context.IsFlagSet(Flag.a1))
                    {
                        Block1(context);
                    }
                }
                while (!context.IsExecutionComplete() && context.IsFlagSetAlarmTriggered());
            }

            private void Block0(ExecutionContext context)
            {
                Best();
                // Mark schedule block executed
                context.SetBlockExecuted(0);
                // Set exit flags
                context.SetFlag(Flag.a1);
            }

            private void Block1(ExecutionContext context)
            {
                Test();
                // Mark schedule block executed
                context.SetBlockExecuted(1);
            }
        }
    }
}