using System;

namespace DialogueScript
{
    public static class Helpers
    {
        public static string GetGeneratedCodeHeader()
        {
            return "// DO NOT EDIT MANUALLY\n" +
                   "// Generated " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + "\n" +
                   "// DO NOT EDIT MANUALLY";
        }
    }
}