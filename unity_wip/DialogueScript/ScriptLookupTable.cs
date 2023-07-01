using System;
using System.Collections.Generic;
using System.Reflection;
using KTrie;

namespace DialogueScript
{
    public static class ScriptLookupTable
    {
        #region Constants
        public const string k_ScriptIdGetter = "ScriptId";
        public const string k_ScriptNameGetter = "ScriptName";
        #endregion

        #region Private Variables
        private static List<Type> s_ScriptIdToType;
        private static StringTrie<int> s_ScriptNameToId;
        #endregion

        #region Initialization
        public static void Initialize()
        {
            s_ScriptIdToType = new();
            s_ScriptNameToId = new();

            // Find all scripts
            // TODO - only search specific namespace
            Type scriptInterfaceType = typeof(Script);
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (scriptInterfaceType.IsAssignableFrom(type) && type.IsValueType)
                    {
                        // Found a Script
                        int scriptId = InvokeScriptIdGetter(type);
                        string scriptName = InvokeScriptNameGetter(type);
                        s_ScriptNameToId[scriptName] = scriptId;

                        // Ensure lookup list is adequately sized
                        while (s_ScriptIdToType.Count <= scriptId) s_ScriptIdToType.Add(null);
                        s_ScriptIdToType[scriptId] = type;
                    }
                }
            }
        }
        #endregion

        #region Lookup Methods
        public static int LookupScriptId(string scriptName) => s_ScriptNameToId[scriptName];
        public static string LookupScriptName(int scriptId)
        {
            Type scriptType = s_ScriptIdToType[scriptId];
            return InvokeScriptNameGetter(scriptType);
        }

        public static Script InstantiateScript(int scriptId)
        {
            Type scriptType = s_ScriptIdToType[scriptId];
            return (Script) Activator.CreateInstance(scriptType);
        }
        #endregion

        #region Helpers
        private static MethodInfo GetMethodUnsafe(Type type, string methodName)
        {
            MethodInfo info = type.GetMethod(methodName);
            if (info == null) throw new Exception($"Could not find method with name {methodName} on type {type}");
            return info;
        }

        private static int InvokeScriptIdGetter(Type type)
            => (int) GetMethodUnsafe(type, k_ScriptIdGetter).Invoke(null, null);
        private static string InvokeScriptNameGetter(Type type)
            => (string) GetMethodUnsafe(type, k_ScriptNameGetter).Invoke(null, null);
        #endregion
    }
}