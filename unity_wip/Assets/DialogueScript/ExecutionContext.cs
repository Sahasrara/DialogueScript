namespace DialogueScript
{
    public class ExecutionContext
    {
        #region Private Variables
        private bool m_FlagSetAlarm;
        private bool m_IsAllSyncExecuted;
        private bool m_IsAllAsyncExecuted;
        private readonly bool[] m_Flags;
        private readonly BlockData[] m_BlockData;
        #endregion

        #region Constructor
        public ExecutionContext(bool[] flags, BlockData[] blockData, bool hasAsyncCode)
        {
            m_Flags = flags;
            m_BlockData = blockData;
            m_FlagSetAlarm = false;
            m_IsAllSyncExecuted = false;
            m_IsAllAsyncExecuted = !hasAsyncCode;
        }
        #endregion

        #region Execution State - Flag Set Alarm
        public void ResetFlagSetAlarm() => m_FlagSetAlarm = false;
        public bool IsFlagSetAlarmTriggered() => m_FlagSetAlarm;
        #endregion

        #region Execution State - Async Functions
        public AsyncFunctionDoneSignal CreateAsyncFunctionCompleteSignal(int blockId, int asyncFunctionId)
            => new(blockId, asyncFunctionId, this);

        private void SetAsyncFunctionComplete(int blockId, int functionId)
        {
            BlockData blockData = m_BlockData[blockId];
            blockData.SetAsyncDone(functionId);
            TriggerFlagsIfNeeded(blockData);

            // Check if all async code is executed
            CheckIfAllAsyncCodeExecuted();
        }
        #endregion

        #region Execution State - Scheduled Block Executed
        public bool IsExecutionComplete() => m_IsAllSyncExecuted && m_IsAllAsyncExecuted;
        public bool IsSynchronousCodeExecuted() => m_IsAllSyncExecuted;
        public bool IsAsynchronousCodeExecuted() => m_IsAllAsyncExecuted;
        public bool IsBlockExecuted(int blockId) => m_BlockData[blockId].SyncDone;
        public void SetBlockExecuted(int blockId)
        {
            BlockData blockData = m_BlockData[blockId];
            blockData.SyncDone = true;
            TriggerFlagsIfNeeded(blockData);

            // Check if all sync code is done
            CheckIfAllSyncCodeExecuted();
        }
        #endregion

        #region Execution State - Flags
        public bool IsFlagSet(int flag) => m_Flags[flag];
        public void SetFlag(int flag)
        {
            m_Flags[flag] = true;
            m_FlagSetAlarm = true;
        }

        private void TriggerFlagsIfNeeded(BlockData blockData)
        {
            if (blockData.AsyncDone)
            {
                // Even if the scheduled block hasn't finished executing, these flags won't be checked until it does.
                blockData.TriggerBlockExitFlags(this);
            }
        }
        #endregion

        #region Helpers
        private void CheckIfAllSyncCodeExecuted()
        {
            foreach (BlockData blockData in m_BlockData)
            {
                if (!blockData.SyncDone) return;
            }
            m_IsAllSyncExecuted = true;
        }

        private void CheckIfAllAsyncCodeExecuted()
        {
            foreach (BlockData block in m_BlockData)
            {
                if (!block.AsyncDone) return;
            }
            m_IsAllAsyncExecuted = true;
        }

        public readonly struct AsyncFunctionDoneSignal
        {
            private readonly int m_BlockId;
            private readonly int m_FunctionId;
            private readonly ExecutionContext m_Context;

            internal AsyncFunctionDoneSignal(int blockId, int functionId, ExecutionContext context)
            {
                m_BlockId = blockId;
                m_FunctionId = functionId;
                m_Context = context;
            }

            public void SignalDone() => m_Context.SetAsyncFunctionComplete(m_BlockId, m_FunctionId);
        }

        public class BlockData
        {
            public bool SyncDone { get; set; }
            public bool AsyncDone { get; set; }
            public bool[] AsyncFunctionCompleteArray { get; set; }
            public System.Action<ExecutionContext> TriggerBlockExitFlags { get; set; }

            public void SetAsyncDone(int index)
            {
                AsyncFunctionCompleteArray[index] = true;
                foreach (bool asyncFunc in AsyncFunctionCompleteArray)
                {
                    if (!asyncFunc)
                    {
                        return;
                    }
                }
                AsyncDone = true;
            }
        }
        #endregion
    }
}