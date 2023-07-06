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
        #endregion

        #region Constructor
        public ExecutionContext(int blockCount)
        {
            m_Flags = new bool[Enum.GetValues(typeof(Flag)).Length];
            m_FlagSetAlarm = false;
            m_BlocksExecuted = new bool[blockCount];
            m_IsExecutionComplete = false;
        }
        #endregion

        #region Execution State - Flag Set Alarm
        public bool ResetFlagSetAlarm() => m_FlagSetAlarm = false;
        public bool IsFlagSetAlarmTriggered() => m_FlagSetAlarm;
        #endregion

        #region Execution State - Scheduled Block Executed
        public bool IsExecutionComplete() => m_IsExecutionComplete;
        public bool IsBlockExecuted(int blockId) => m_BlocksExecuted[blockId];
        public void SetBlockExecuted(int blockId)
        {
            m_BlocksExecuted[blockId] = true;

            // If all blocks are executed, mark execution complete
            bool complete = true;
            for (int i = 0; i < m_BlocksExecuted.Length; i++)
            {
                if (!m_BlocksExecuted[i])
                {
                    complete = false;
                    break;
                }
            }
            if (complete) m_IsExecutionComplete = true;
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

    }
}