using System.Collections;
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

        static void Log(string logString)
        {
            Debug.Log(logString);
        }

        static void TestAsync(ExecutionContext.AsyncFunctionDoneSignal completeSignal)
        {
            CoroutineBuddy.StartRoutine(WaitForSeconds(completeSignal, 4));
        }

        static IEnumerator WaitForSeconds(ExecutionContext.AsyncFunctionDoneSignal completeSignal, int seconds)
        {
            Debug.Log("Starting Wait");
            yield return new WaitForSeconds(seconds);
            Debug.Log("Ending Wait");
            completeSignal.SignalDone();
        }
    }
}