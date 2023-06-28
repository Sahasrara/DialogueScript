using System.Collections.Generic;

namespace DialogueScript
{
    public class ExecutionContext
    {
        #region Private Variables
        private bool m_FlagSetAlarm;
        private List<bool> m_Flags;
        #endregion

        #region Constructor
        public ExecutionContext()
        {
            m_Flags = new();
            m_FlagSetAlarm = false;
        }
        #endregion

        #region Execution State - Initialize / Reset
        public void Initialize(int blockCount, int flagCount)
        {
            Reset();

            // Flags
            for (int i = 0; i < flagCount; i++) m_Flags.Add(false);
        }

        public void Reset()
        {
            m_Flags.Clear();
            m_FlagSetAlarm = false;
        }
        #endregion

        #region Execution State - Flag Set Alarm
        public bool ResetFlagSetAlarm() => m_FlagSetAlarm = false;
        public bool TriggerFlagSetAlarm() => m_FlagSetAlarm = true;
        public bool IsFlagSetAlarmTriggered() => m_FlagSetAlarm;
        #endregion

        #region Execution State - Block Executed
        public bool IsBlockExecuted(int blockId)
        {
            return true;
        }
        #endregion

        #region Execution State - Flags
        public void SetFlag(int flagId) => m_Flags[flagId] = true;
        public bool IsFlagSet(int flagId) => m_Flags[flagId];
        #endregion

    }
}