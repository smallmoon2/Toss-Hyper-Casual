using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class StageBase : MonoBehaviour
{
    [Header("Stage 공통 설정")]
    public StageManager stageManager;
    public GameObject clearAction;
    public GameObject failAction;
    public GameObject startPos;
    public GameObject endPos;
    public GameObject player;
    public Image prograssbar;

    protected float finishTime = 0f;
    public int level;
    protected float playTime = 5f;
    protected float balance = 0f;
    protected float endingTime = 2f;
    public bool isClear = false;
    protected bool timeclear = false;
    protected bool isFinshed = false;

    protected float maxPlayTime = 5f;  // 최대 시간
    protected float minPlayTime = 3f;  // 최소 시간

    protected Vector3 startPosition;
    protected Vector3 endPosition;

    protected virtual void OnEnable()
    {
        isFinshed = false;
        timeclear = false;
        clearAction.SetActive(false);
        failAction.SetActive(false);
        isClear = false;

        level = stageManager.StageLevel;

        balance = (maxPlayTime - minPlayTime) / 3f; // 5레벨 → 4단계 변화
        playTime = maxPlayTime - (level - 1) * balance;

        startPosition = startPos.transform.position;
        endPosition = endPos.transform.position;
        player.transform.position = startPosition;

        StartCoroutine(UpdateProgressBar());
    }

    protected virtual IEnumerator UpdateProgressBar()
    {
        prograssbar.fillAmount = 1f;

        while (prograssbar.fillAmount > 0f)
        {
            prograssbar.fillAmount -= Time.deltaTime / playTime;
            yield return null;
        }

        prograssbar.fillAmount = 0f;

        if (timeclear)
        {
            MissionClear();
        }
        else
        {
            StartCoroutine(FailEnding());
        }
    }

    protected virtual void MissionClear()
    {
        if (isFinshed) return;  //  중복 방지
        isFinshed = true;

        StopAllCoroutines();
        StartCoroutine(ClearEnding());
    }

    protected virtual IEnumerator ClearEnding()
    {
        isClear = true;
        stageManager.scoreNum += 100;
        prograssbar.fillAmount = 1f;

        yield return new WaitForSeconds(finishTime);
        clearAction.SetActive(true);
        Setreset();

        yield return new WaitForSeconds(endingTime);
        stageManager.isStagenext = true;
    }

    protected virtual IEnumerator FailEnding()
    {
        if (isFinshed) yield break;  //  중복 방지
        isFinshed = true;

        prograssbar.fillAmount = 1f;
        yield return new WaitForSeconds(finishTime);
        failAction.SetActive(true);
        stageManager.Life--;
        Setreset();

        yield return new WaitForSeconds(endingTime);
        stageManager.isStagenext = true;
    }

    protected virtual void Setreset()
    {
        // 필요한 리셋 처리 구현 가능
    }
}
