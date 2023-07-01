using System;
using System.Collections.Generic;

namespace DialogueScript
{
    // TODO - this code is all garbage and needs to be written
    public class ExecutionContext
    {
        #region Private Variables
        private bool m_FlagSetAlarm;
        private bool[] m_Flags;
        #endregion

        #region Constructor
        public ExecutionContext()
        {
            m_Flags = new bool[Enum.GetValues(typeof(Flag)).Length];
            m_FlagSetAlarm = false;
        }
        #endregion

        #region Execution State - Initialize / Reset
        public void Initialize(int blockCount, int flagCount)
        {
            Reset();

            // Flags
            // for (int i = 0; i < flagCount; i++) m_Flags.Add(false);
            // TODO
        }

        public void Reset()
        {
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
        public void SetFlag(Flag flag) => m_Flags[(int)flag] = true;
        public bool IsFlagSet(Flag flag) => m_Flags[(int)flag];
        #endregion

    }
}