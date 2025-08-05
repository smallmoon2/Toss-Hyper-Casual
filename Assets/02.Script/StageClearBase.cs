using UnityEngine;
using System.Collections;

public abstract class StageClearBase : MonoBehaviour
{
    public StageBase StageBase;
    protected  bool IsStageClear;

    protected virtual void OnEnable()
    {

        if (StageBase.isClear)
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
