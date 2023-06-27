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
            // Read test file and generate C# from DialogueScript
            string testDirectory = Path.Combine(Application.dataPath, "DialogueScript");
            string testFilePath = Path.Combine(testDirectory, "TestScript.ds");
            if (!File.Exists(testFilePath)) Debug.LogError("Could not find test_script.ds");
            string testFileString = File.ReadAllText(testFilePath);

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
            DialogueScriptListenerTranspiler listenerTranspiler = new("TestNamespace", "TestClass");
            ParseTreeWalker.Default.Walk(listenerTranspiler, tree);

            // Create New Script
            string testScriptPath = Path.Combine(testDirectory, "Generated", "TestScript.cs");
            string dialogueScript = listenerTranspiler.ToString();
            File.WriteAllText(testScriptPath, dialogueScript);
            AssetDatabase.Refresh();
        }

        [MenuItem("DialogueScript/Execute Test Script")]
        public static void ExecuteTestScript()
        {
            // TODO
        }
    }
}