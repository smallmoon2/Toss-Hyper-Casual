using UnityEngine;
using System.Collections;

public abstract class StageClearBase : MonoBehaviour
{
    protected abstract bool IsStageClear();

    protected virtual void OnEnable()
    {
        if (IsStageClear())
            StartCoroutine(HandleClear());
        else
            StartCoroutine(HandleFail());
    }

    private IEnumerator HandleClear()
    {
        yield return StartCoroutine(Clear());
    }

    private IEnumerator HandleFail()
    {
        yield return StartCoroutine(Fail());
    }

    protected abstract IEnumerator Clear();
    protected abstract IEnumerator Fail();
}
