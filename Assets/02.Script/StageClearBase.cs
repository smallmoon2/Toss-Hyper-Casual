using UnityEngine;
using System.Collections;

public abstract class StageClearBase : MonoBehaviour
{
    public StageBase StageBase;
    protected  bool IsStageClear;

    protected virtual void OnEnable()
    {
        Debug.Log("클리어 베이스생성됨");
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
