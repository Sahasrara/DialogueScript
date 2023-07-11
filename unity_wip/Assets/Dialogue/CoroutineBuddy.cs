using System.Collections;
using UnityEngine;

public class CoroutineBuddy : MonoBehaviour
{
    private static CoroutineBuddy s_Instance;

    private void Awake() => s_Instance = this;

    public static Coroutine StartRoutine(IEnumerator enumerator) => s_Instance.StartCoroutine(enumerator);
    public static void StopRoutine(Coroutine routine) => s_Instance.StopCoroutine(routine);
}