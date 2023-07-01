using System.Collections.Generic;
using System.IO;

namespace DialogueScript
{
    public class FlagCache
    {
        #region Constants
        private const string k_FlagListEnd = "/* END FLAG LIST */";
        private const string k_FlagListStart = "/* START FLAG LIST */";
        #endregion

        #region Private Variables
        private List<string> m_FlagList;
        private HashSet<string> m_FlagSet;
        private string m_PathToCacheCode;
        #endregion

        #region Constructor
        public FlagCache(string pathToCacheCode)
        {
            // Set file path
            m_PathToCacheCode = pathToCacheCode;

            // Create backing data structures
            m_FlagSet = new();
            m_FlagList = new();

            // Grab cache file
            EnsureCacheFileExists();

            // Read
            ReadFileToCache();
        }
        #endregion

        #region Cache Methods
        public void AddFlag(string flag)
        {
            if (m_FlagSet.Contains(flag)) return;
            m_FlagSet.Add(flag);
            m_FlagList.Add(flag);
        }

        public void GenerateFlags()
        {
            using(StreamWriter writer = new StreamWriter(m_PathToCacheCode))
            {
                writer.WriteLine(Helpers.GetGeneratedCodeHeader());
                writer.WriteLine();
                writer.WriteLine("namespace DialogueScript");
                writer.WriteLine("{");
                writer.WriteLine("    public enum Flag");
                writer.WriteLine("    {");
                writer.WriteLine(k_FlagListStart);
                for (int i = 0; i < m_FlagList.Count; i++)
                {
                    writer.Write("        ");
                    writer.Write(m_FlagList[i]);
                    writer.Write(",\n");
                }
                writer.WriteLine(k_FlagListEnd);
                writer.WriteLine("    }");
                writer.WriteLine("}");
            }
        }
        #endregion

        #region Helpers
        private void EnsureCacheFileExists()
        {
            if (!File.Exists(m_PathToCacheCode)) GenerateFlags();
        }

        private void ReadFileToCache()
        {
            using(StreamReader reader = new StreamReader(m_PathToCacheCode))
            {
                // Find list start
                for (string line = reader.ReadLine(); line != k_FlagListStart; line = reader.ReadLine()) { }

                // Read
                for (string line = reader.ReadLine(); line != k_FlagListEnd; line = reader.ReadLine())
                {
                    string trimmedLineWithoutComma = line.Substring(0, line.Length - 1).Trim();
                    AddFlag(trimmedLineWithoutComma);
                }
            }
        }
        #endregion
    }
}