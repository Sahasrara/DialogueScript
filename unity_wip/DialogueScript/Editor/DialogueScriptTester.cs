using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using UnityEditor;
using UnityEngine;

namespace DialogueScript.Editor
{
    public class DialogueScriptTester
    {
        [MenuItem("DialogueScript/Generate Test Script")]
        public static void GenerateTestScript()
        {
            // Class and namespace name
            string testClassName = "TestClass";
            string testScriptName = "TestScript";
            int testScriptId = 0;

            // Grab file paths
            string testDirectory = Path.Combine(Application.dataPath, "DialogueScript");
            string testFilePath = Path.Combine(testDirectory, "TestScript.ds");
            string testScriptPath = Path.Combine(testDirectory, "generated", "TestScript.cs");
            string testFlagPath = Path.Combine(testDirectory, "generated", "Flag.cs");
            if (!File.Exists(testFilePath)) Debug.LogError("Could not find test_script.ds");

            // Create flag cache
            FlagCache flagCache = new(testFlagPath);

            // Generate script
            GenerateScript(flagCache, testFilePath, testScriptPath, testClassName, testScriptName,
                testScriptId);
        }

        [MenuItem("DialogueScript/Execute Test Script")]
        public static void ExecuteTestScript()
        {
            // Initialize Lookup Table
            ScriptLookupTable.Initialize();

            // TODO - Execute a test script
        }

        private static void GenerateScript(FlagCache flagCache, string dialogueScriptSourcePath,
            string generatedCodePath, string className, string scriptName, int scriptId)
        {
            // Read test file and generate C# from DialogueScript
            string testFileString = File.ReadAllText(dialogueScriptSourcePath);

            // Generate DialogueScript Parse Tree
            ICharStream stream = CharStreams.fromString(testFileString);
            ITokenSource lexer = new DialogueScriptLexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            DialogueScriptParser parser = new(tokens)
            {
                BuildParseTree = true,
                ErrorHandler = new BailErrorStrategy(),
            };
            DialogueScriptParser.ScriptContext tree = parser.script();

            // Visit the Tree
            string dialogueScript = TranspilingTreeWalker.WalkScript(
                tree, flagCache, className, scriptName, scriptId);

            // Create New Script
            File.WriteAllText(generatedCodePath, dialogueScript);

            // Write Flag Cache
            flagCache.GenerateFlags();

            // Refresh asset database
            AssetDatabase.Refresh();
        }
    }
}