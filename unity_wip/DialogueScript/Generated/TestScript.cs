// DO NOT EDIT MANUALLY
// Generated Thursday, 06 July 2023 18:25:03
// DO NOT EDIT MANUALLY
namespace DialogueScript
{
    public static partial class Functions
    {
        public struct TestClass : IScript
        {
            public static int ScriptId() => 0;
            public static string ScriptName() => "TestScript";
            public ExecutionContext CreateExecutionContext()
            {
                int blockCount = 3;
                bool[][] asyncFunctionCompleteArray = new bool[blockCount][];
                asyncFunctionCompleteArray[0] = new bool[0];
                asyncFunctionCompleteArray[1] = new bool[1];
                asyncFunctionCompleteArray[2] = new bool[0];
                return new(blockCount, asyncFunctionCompleteArray);
            }

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

                    // Scheduled Block - 2
                    if (!context.IsBlockExecuted(2) && context.IsFlagSet(Flag.asyncDone))
                    {
                        Block2(context);
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
                TestAsync(context.CreateAsyncFunctionCompleteSignal(1, 0));
                // Mark schedule block executed
                context.SetBlockExecuted(1);
                // Set exit flags
                context.SetFlag(Flag.asyncDone);
            }

            private void Block2(ExecutionContext context)
            {
                Best();
                // Mark schedule block executed
                context.SetBlockExecuted(2);
            }
        }
    }
}