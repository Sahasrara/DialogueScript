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
            string testNamespaceName = "TestNamespace";
            string testClassName = "TestClass";

            // Grab file paths
            string testDirectory = Path.Combine(Application.dataPath, "DialogueScript");
            string testFilePath = Path.Combine(testDirectory, "TestScript.ds");
            string testScriptPath = Path.Combine(testDirectory, "Generated", "TestScript.cs");
            if (!File.Exists(testFilePath)) Debug.LogError("Could not find test_script.ds");

            // Generate script
            GenerateScript(testFilePath, testScriptPath, testNamespaceName, testClassName, 0);
        }

        [MenuItem("DialogueScript/Execute Test Script")]
        public static void ExecuteTestScript()
        {
            // Initialize Lookup Table
            ScriptLookupTable.Initialize();

            // TODO - Execute a test script
        }

        private static void GenerateScript(string dialogueScriptSourcePath, string generatedCodePath,
            string namespaceName, string className, int scriptId)
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
            IParseTree tree = parser.script();

            // Visit the Tree
            TranspilerListener listener = new(namespaceName, className, scriptId);
            ParseTreeWalker.Default.Walk(listener, tree);

            // Create New Script
            string dialogueScript = listener.ToString();
            File.WriteAllText(generatedCodePath, dialogueScript);
            AssetDatabase.Refresh();
        }
    }
}