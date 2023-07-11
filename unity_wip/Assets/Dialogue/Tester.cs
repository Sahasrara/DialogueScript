using DialogueScript;
using UnityEngine;

public class Tester : MonoBehaviour
{
    private IScript m_Script;
    private ExecutionContext m_Context;

    private void Start()
    {
        // Initialize Lookup Table
        ScriptLookupTable.Initialize();

        // Find Script
        int scriptId = ScriptLookupTable.LookupScriptId("TestScript");
        m_Script = ScriptLookupTable.InstantiateScript(scriptId);

        // Create Execution Context
        m_Context = m_Script.CreateExecutionContext();
    }

    private void Update()
    {
        // Execute Script
        if (m_Context.IsExecutionComplete()) Destroy(this);
        m_Script.Tick(m_Context);
    }
}