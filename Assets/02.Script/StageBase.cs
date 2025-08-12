using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class StageBase : MonoBehaviour
{
    [Header("Stage ���� ����")]
    public StageManager stageManager;
    public GameObject clearAction;
    public GameObject failAction;
    public GameObject startPos;
    public GameObject endPos;
    public GameObject player;
    public Image prograssbar;


    public bool eggStage = false;
    protected float finishTime = 0f;
    public int level;
    protected float playTime = 5f;
    protected float balance = 0f;
    protected float endingTime = 2f;
    public bool isClear = false;
    protected bool timeclear = false;
    protected bool isFinshed = false;

    protected float maxPlayTime = 5f;  // �ִ� �ð�
    protected float minPlayTime = 3f;  // �ּ� �ð�

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

        balance = (maxPlayTime - minPlayTime) / 3f; // 5���� �� 4�ܰ� ��ȭ
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
        if (isFinshed) return;
        isFinshed = true;

        // ���� �ð� ����(0~1) �� timbonus(0~30)
        float remainingRatio = Mathf.Clamp01(prograssbar.fillAmount);
        stageManager.timbonus = Mathf.RoundToInt(remainingRatio * 30f);

        StopAllCoroutines();
        StartCoroutine(ClearEnding());
    }

    protected virtual IEnumerator ClearEnding()
    {
        isClear = true;

        // �⺻ 100�� + �ð� ���ʽ�
        stageManager.scoreNum += 100 + stageManager.timbonus;

        prograssbar.fillAmount = 1f;

        yield return new WaitForSeconds(finishTime);
        clearAction.SetActive(true);
        Setreset();

        yield return new WaitForSeconds(endingTime);
        clearAction.SetActive(false);
        failAction.SetActive(false);

        stageManager.isStagenext = true;


    }

    protected virtual IEnumerator FailEnding()
    {
        if (isFinshed) yield break;  //  �ߺ� ����
        isFinshed = true;

        prograssbar.fillAmount = 1f;
        yield return new WaitForSeconds(finishTime);
        failAction.SetActive(true);
        stageManager.Life--;
        Setreset();
        stageManager.timbonus = 0;
        yield return new WaitForSeconds(endingTime);
        clearAction.SetActive(false);
        failAction.SetActive(false);

        stageManager.isStagenext = true;
    }

    protected virtual void Setreset()
    {
        // �ʿ��� ���� ó�� ���� ����
    }
}
