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

    protected int level;
    protected float playTime = 5f;
    protected float endingTime = 2f;
    

    protected Vector3 startPosition;
    protected Vector3 endPosition;

    protected virtual void OnEnable()
    {

        clearAction.SetActive(false);
        failAction.SetActive(false);

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
        StartCoroutine(FailEnding());

    }

    protected virtual void MissionClear()
    {

        StopAllCoroutines();
        prograssbar.fillAmount = 1f;
        //enabled = false;
        StartCoroutine(ClearEnding());
    }

    protected virtual IEnumerator ClearEnding()
    {
        yield return new WaitForSeconds(1f);
        clearAction.SetActive(true);
        yield return new WaitForSeconds(endingTime);
        stageManager.isStagenext = true;
    }

    protected virtual IEnumerator FailEnding()
    {
        yield return new WaitForSeconds(1f);
        failAction.SetActive(true);
        stageManager.Life--;
        yield return new WaitForSeconds(endingTime);
        
        stageManager.isStagenext = true;
    }
}
