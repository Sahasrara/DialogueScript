using System;

namespace DialogueScript
{
    public class ExecutionContext
    {
        #region Private Variables
        private bool m_FlagSetAlarm;
        private bool m_IsExecutionComplete;
        private bool[] m_Flags;
        private bool[] m_BlocksExecuted;
        private bool[][] m_AsyncFunctionsCompleted; // [blockId, functionId]
        #endregion

        #region Constructor
        public ExecutionContext(int blockCount, bool[][] asyncFunctionCompleteArray)
        {
            m_Flags = new bool[Enum.GetValues(typeof(Flag)).Length];
            m_FlagSetAlarm = false;
            m_BlocksExecuted = new bool[blockCount];
            m_IsExecutionComplete = false;
            m_AsyncFunctionsCompleted = asyncFunctionCompleteArray;
        }
        #endregion

        #region Execution State - Flag Set Alarm
        public void ResetFlagSetAlarm() => m_FlagSetAlarm = false;
        public bool IsFlagSetAlarmTriggered() => m_FlagSetAlarm;
        #endregion

        #region Execution State - Async Functions
        private Action<int, int> m_AsyncFunctionCompleteDelegate;
        private Action<int, int> SetAsyncFunctionCompleteDelegate
            => m_AsyncFunctionCompleteDelegate ??= SetAsyncFunctionComplete; // Cache the delegate

        internal AsyncFunctionCompleteSignal CreateAsyncFunctionCompleteSignal(int blockId, int asyncFunctionId)
            => new(blockId, asyncFunctionId, SetAsyncFunctionCompleteDelegate);

        private void SetAsyncFunctionComplete(int blockId, int functionId)
        {
            m_AsyncFunctionsCompleted[blockId][functionId] = true;

            // If all async functions are complete, mark block complete
            if (IsBoolArrayAllTrue(m_AsyncFunctionsCompleted[blockId]))
            {
                SetBlockExecuted(blockId);
            }
        }
        #endregion

        #region Execution State - Scheduled Block Executed
        public bool IsExecutionComplete() => m_IsExecutionComplete;
        public bool IsBlockExecuted(int blockId) => m_BlocksExecuted[blockId];
        public void SetBlockExecuted(int blockId)
        {
            m_BlocksExecuted[blockId] = true;

            // If all blocks are executed, mark execution complete
            if (IsBoolArrayAllTrue(m_BlocksExecuted))
            {
                m_IsExecutionComplete = true;
            }
        }
        #endregion

        #region Execution State - Flags
        public bool IsFlagSet(Flag flag) => m_Flags[(int)flag];
        public void SetFlag(Flag flag)
        {
            m_Flags[(int)flag] = true;
            m_FlagSetAlarm = true;
        }
        #endregion

        #region Helpers
        private bool IsBoolArrayAllTrue(bool[] boolArray)
        {
            for (int i = 0; i < boolArray.Length; i++)
            {
                if (!boolArray[i])
                {
                    return false;
                }
            }
            return true;
        }

        public readonly struct AsyncFunctionCompleteSignal
        {
            private readonly int m_BlockId;
            private readonly int m_FunctionId;
            private readonly Action<int, int> m_CompleteFunction;

            internal AsyncFunctionCompleteSignal(int blockId, int functionId, Action<int, int> completeFunction)
            {
                m_BlockId = blockId;
                m_FunctionId = functionId;
                m_CompleteFunction = completeFunction;
            }

            public void SignalComplete() => m_CompleteFunction(m_BlockId, m_FunctionId);
        }
        #endregion
    }
}