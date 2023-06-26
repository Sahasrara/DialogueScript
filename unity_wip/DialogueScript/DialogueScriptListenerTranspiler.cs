using System.Text;

namespace DialogueScript
{
    public class DialogueScriptListenerTranspiler : DialogueScriptParserBaseListener
    {
        #region Private Variables
        private string m_Namespace;
        private StringBuilder m_StringBuilder;
        #endregion

        #region Constructor
        public DialogueScriptListenerTranspiler(string namespaceString = null)
        {
            // Write Warning Header
            m_StringBuilder = new();
            m_StringBuilder.AppendLine("// DO NOT EDIT MANUALLY");
            m_StringBuilder.AppendLine("// Generated " + System.DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
            m_StringBuilder.AppendLine("// DO NOT EDIT MANUALLY");
            m_StringBuilder.AppendLine();

            // Namespace
            m_Namespace = namespaceString;
        }
        #endregion

        #region ToString
        public override string ToString() => m_StringBuilder.ToString();
        #endregion

        #region Visitor Methods - Script
        public override void EnterScript(DialogueScriptParser.ScriptContext context)
        {
            if (string.IsNullOrEmpty(m_Namespace)) return;
            m_StringBuilder.AppendLine($"namespace {m_Namespace}");
            m_StringBuilder.AppendLine("{");
        }

        public override void ExitScript(DialogueScriptParser.ScriptContext context)
        {
            if (string.IsNullOrEmpty(m_Namespace)) return;
            m_StringBuilder.AppendLine("}");
        }
        #endregion

        #region Visitor Methods - Function Invocation
        // TODO - This is where I left off
        // public override void EnterExpression_postfix_invoke(
        //     DialogueScriptParser.Expression_postfix_invokeContext context)
        // {
        //
        //
        // }
        #endregion
    }
}
