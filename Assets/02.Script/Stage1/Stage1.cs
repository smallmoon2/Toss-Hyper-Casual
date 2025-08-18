using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stage1 : StageBase
{
    public Door_Screen door;
    private int touchCount = 0;
    public int maxTouches = 10;
    private Animator anim;

    public bool first;
    
    protected override void OnEnable()
    {
        finishTime = 1f;
        touchCount = 0;
        maxTouches = 10;
        playTime = 5f;
        endingTime = 2f;
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
        if(first)
        {
            StartCoroutine(UpdateProgressBar());
        }


        SoundManager.Instance.Play("Subway_Open");
        door.OpenDoor();

        anim = player.GetComponent<Animator>();
    }

    private void Update()
    {

        if (!PauseManager.Instance || !PauseManager.Instance.IsPaused)
        {
            if (Input.GetMouseButtonDown(0) ||
(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                if (!first)
                {
                    SoundManager.Instance.SetBGMVolume(0.02f);

                    StartCoroutine(UpdateProgressBar());
                    first = true;
                }

                if (!isFinshed)
                {
                    HandleTouch();
                }

            }
        }

    }

    private void HandleTouch()
    {
        if (touchCount >= maxTouches) return;

        touchCount++;
        float t = touchCount / (float)maxTouches;
        Vector3 targetPos = Vector3.Lerp(startPosition, endPosition, t);
        StartCoroutine(MoveToPosition(targetPos, 0.3f));
    }

    private IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        Vector3 initialPos = player.transform.position;
        float elapsed = 0f;

        //  애니메이션 시작: IsRun = true
        
        if (anim != null)
        {
            anim.SetBool("IsRun", true);
            SoundManager.Instance.PlayLoop("Walk");
        }

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            player.transform.position = Vector3.Lerp(initialPos, target, t);
            yield return null;
        }

        player.transform.position = target;



        if (touchCount >= maxTouches && Vector3.Distance(target, endPosition) < 0.01f)
        {
            MissionClear();
        }
    }


    protected override void MissionClear()
    {
        if (isFinshed) return;
        isFinshed = true;

        if (anim != null)
        {
            SoundManager.Instance.Stop();
            anim.SetBool("IsRun", false);
        }

        if (anim != null)
        {
            anim.SetTrigger("Isboarding");
        }
        SoundManager.Instance.Play("Subway_Open");
        door.CloseDoor();           // 먼저 문 닫기

        float remainingRatio = Mathf.Clamp01(prograssbar.fillAmount);
        stageManager.timbonus = Mathf.RoundToInt(remainingRatio * 30f);

        StopAllCoroutines();
        StartCoroutine(ClearEnding());

    }
    protected override IEnumerator UpdateProgressBar()
    {
 
        // 먼저 문 닫기
        yield return base.UpdateProgressBar();
        SoundManager.Instance.Play("Subway_Open");
        door.CloseDoor();
        if (anim != null)
        {
            anim.SetBool("IsFowardDown", true);
        }
        isFinshed = true;
    }

    protected override IEnumerator FailEnding()
    {
        if (isFinshed) yield break;  //  중복 방지
        isFinshed = true;

        prograssbar.fillAmount = 1f;
        anim.SetBool("IsRun", false);
        SoundManager.Instance.Stop();
        yield return new WaitForSeconds(finishTime);
        failAction.SetActive(true);
        stageManager.Life--;

        yield return new WaitForSeconds(endingTime);

        Debug.Log("오버라이딩");
        anim.SetBool("IsFowardDown", false);
        stageManager.isStagenext = true;
    }

}