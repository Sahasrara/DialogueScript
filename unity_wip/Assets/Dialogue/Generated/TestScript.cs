// DO NOT EDIT MANUALLY
// Generated Monday, 10 July 2023 19:57:13
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
                int blockCount = 5;
                ExecutionContext.BlockData[] blockData = new ExecutionContext.BlockData[blockCount];
                blockData[0] = new ExecutionContext.BlockData();
                blockData[0].AsyncFunctionCompleteArray = new bool[0];
                blockData[0].TriggerBlockExitFlags = Block0ExitFlags;
                blockData[0].AsyncDone = true;
                blockData[1] = new ExecutionContext.BlockData();
                blockData[1].AsyncFunctionCompleteArray = new bool[1];
                blockData[1].TriggerBlockExitFlags = Block1ExitFlags;
                blockData[2] = new ExecutionContext.BlockData();
                blockData[2].AsyncFunctionCompleteArray = new bool[1];
                blockData[2].TriggerBlockExitFlags = Block2ExitFlags;
                blockData[3] = new ExecutionContext.BlockData();
                blockData[3].AsyncFunctionCompleteArray = new bool[0];
                blockData[3].TriggerBlockExitFlags = Block3ExitFlags;
                blockData[3].AsyncDone = true;
                blockData[4] = new ExecutionContext.BlockData();
                blockData[4].AsyncFunctionCompleteArray = new bool[0];
                blockData[4].TriggerBlockExitFlags = Block4ExitFlags;
                blockData[4].AsyncDone = true;
                return new(new bool[(int)Flag.RESERVED_FLAG_COUNT], blockData, true);
            }

            public void Tick(ExecutionContext context)
            {
                if (context.IsSynchronousCodeExecuted())
                    return;
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
                    if (!context.IsBlockExecuted(1) && context.IsFlagSet((int)Flag.a1))
                    {
                        Block1(context);
                    }

                    // Scheduled Block - 2
                    if (!context.IsBlockExecuted(2) && context.IsFlagSet((int)Flag.asyncDone))
                    {
                        Block2(context);
                    }

                    // Scheduled Block - 3
                    if (!context.IsBlockExecuted(3) && context.IsFlagSet((int)Flag.secondAsyncDone))
                    {
                        Block3(context);
                    }

                    // Scheduled Block - 4
                    if (!context.IsBlockExecuted(4) && context.IsFlagSet((int)Flag.DoubleFlag))
                    {
                        Block4(context);
                    }
                }
                while (!context.IsSynchronousCodeExecuted() && context.IsFlagSetAlarmTriggered());
            }

            private void Block0(ExecutionContext context)
            {
                Log("first block");
                // Mark schedule block executed
                context.SetBlockExecuted(0);
            }

            private void Block0ExitFlags(ExecutionContext context)
            {
                context.SetFlag((int)Flag.a1);
            }

            private void Block1(ExecutionContext context)
            {
                Log("second block");
                TestAsync(context.CreateAsyncFunctionCompleteSignal(1, 0));
                // Mark schedule block executed
                context.SetBlockExecuted(1);
            }

            private void Block1ExitFlags(ExecutionContext context)
            {
                context.SetFlag((int)Flag.asyncDone);
            }

            private void Block2(ExecutionContext context)
            {
                Log("third block");
                TestAsync(context.CreateAsyncFunctionCompleteSignal(2, 0));
                // Mark schedule block executed
                context.SetBlockExecuted(2);
            }

            private void Block2ExitFlags(ExecutionContext context)
            {
                context.SetFlag((int)Flag.secondAsyncDone);
                context.SetFlag((int)Flag.DoubleFlag);
            }

            private void Block3(ExecutionContext context)
            {
                Log("fourth block");
                // Mark schedule block executed
                context.SetBlockExecuted(3);
            }

            private void Block3ExitFlags(ExecutionContext context)
            {
            }

            private void Block4(ExecutionContext context)
            {
                Log("fifth block");
                // Mark schedule block executed
                context.SetBlockExecuted(4);
            }

            private void Block4ExitFlags(ExecutionContext context)
            {
            }
        }
    }
}