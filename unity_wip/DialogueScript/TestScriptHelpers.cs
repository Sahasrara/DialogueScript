using UnityEngine;

namespace DialogueScript
{
    public static partial class Functions
    {
        static void Best()
        {
            Debug.Log("Best");
        }

        static void Test()
        {
            Debug.Log("Test");
        }

        static void TestAsync(ExecutionContext.AsyncFunctionCompleteSignal completeSignal)
        {
            completeSignal.SignalComplete();
        }
    }
}