using System.Collections;
using UnityEngine;

public class CoroutineManager : Singleton<CoroutineManager>
{
    public Coroutine StartRoutine(IEnumerator enumerator)
    {
        return StartCoroutine(enumerator);
    }
    public void StopRoutine(Coroutine routine)
    {
        if (routine != null)
            StopCoroutine(routine);
    }
}
