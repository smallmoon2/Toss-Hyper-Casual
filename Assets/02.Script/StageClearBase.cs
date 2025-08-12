using UnityEngine;
using System.Collections;

public abstract class StageClearBase : MonoBehaviour
{
    public StageBase StageBase;

    protected  bool IsStageClear;
    public GameObject egg;
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
        if (StageBase.eggStage)
        {
            SoundManager.HelpCount++;

            if (SoundManager.HelpCount >= 3)
            {
                egg.SetActive(true);
            }
        }
        yield return StartCoroutine(Clear());

    }

    private IEnumerator HandleFail()
    {
        if (StageBase.eggStage)
        {
            egg.SetActive(false);
            SoundManager.HelpCount = 0;
        }
        yield return StartCoroutine(Fail());

    }

    protected abstract IEnumerator Clear();
    protected abstract IEnumerator Fail();
}
