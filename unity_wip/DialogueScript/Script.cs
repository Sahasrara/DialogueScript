using System;

namespace DialogueScript
{
    /// <summary>
    /// Scripts represent DialogueScript code "scripts".
    /// These are to be instantiated and tick called until execution completes.
    /// TODO - we're missing some important methods here like "IsExecuted()" so we know when to stop
    /// TODO - we may also want to allow interfering with flags, but for now we'll leave that out
    /// </summary>
    public interface Script
    {
        public static int FlagCount() => throw new Exception("Could not find FlagCount implementation");
        public static int ScriptId() => throw new Exception("Could not find ScriptId implementation");
        public static string ScriptName => throw new Exception("Could not find ScriptName implementation");
        public void Tick(ExecutionContext context);
    }
}

