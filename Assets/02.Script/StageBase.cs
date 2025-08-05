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
    protected int level;
    protected float playTime = 5f;
    protected float endingTime = 2f;
    public bool isClear = false;
    protected bool timeclear = false;
    protected bool isFinshed = false;

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
        playTime -= 0.7f * level;

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

        StopAllCoroutines();
        
        //enabled = false;
        StartCoroutine(ClearEnding());
    }

    protected virtual IEnumerator ClearEnding()
    {
        isClear = true;
        stageManager.scoreNum = stageManager.scoreNum + 100;
        prograssbar.fillAmount = 1f;
        yield return new WaitForSeconds(finishTime);
        clearAction.SetActive(true);
        yield return new WaitForSeconds(endingTime);
        stageManager.isStagenext = true;
    }

    protected virtual IEnumerator FailEnding()
    {

        prograssbar.fillAmount = 1f;
        yield return new WaitForSeconds(finishTime);
        failAction.SetActive(true);
        stageManager.Life--;
        yield return new WaitForSeconds(endingTime);
        
        stageManager.isStagenext = true;
    }

    
}
