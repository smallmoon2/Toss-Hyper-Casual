using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stage7 : StageBase
{
    public MaskController maskController;
    private int touchCount = 0;
    public int maxTouches = 10;
    private float touchtime = 0.3f;
    private float walkDuration = 0.5f;

    private float lastTouchTime = -10f;
    private bool isFailing = false;

    private Animator anim;
    private Coroutine walkResetCoroutine;

    protected override void OnEnable()
    {
        maxPlayTime = 7f;  // �ִ� �ð�
        minPlayTime = 4.5f;  // �ּ� �ð�
        maskController.ActivateMaskChild(0);
        anim = player.GetComponent<Animator>();


        if (walkResetCoroutine != null)
        {
            StopCoroutine(walkResetCoroutine);
            walkResetCoroutine = null;
        }

        finishTime = 1f;
        touchCount = 0;
        maxTouches = 10;
        playTime = 5f;
        endingTime = 1f;
        lastTouchTime = -10f;
        isFailing = false;

        base.OnEnable();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) ||
            (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            HandleTouch();
        }
    }

    private void HandleTouch()
    {
        if (isFailing || touchCount >= maxTouches) return;

        float now = Time.time;

        if (now - lastTouchTime < touchtime)
        {
            Debug.Log("������ �� �� ��ġ�� �� �����̵�");
 
            anim.SetBool("IsSliding", true);
            StartCoroutine(FailEnding());
            isFailing = true;
            return;
        }

        lastTouchTime = now;

        // �ȱ� �ִϸ��̼� Bool
        anim.SetBool("IsWalkB", true);

        // ���� �ڷ�ƾ ���� �� ���� ����
        if (walkResetCoroutine != null)
        {
            StopCoroutine(walkResetCoroutine);
        }
        walkResetCoroutine = StartCoroutine(WalkBoolResetTimer());

        // ������
        touchCount++;
        float t = touchCount / (float)maxTouches;
        Vector3 targetPos = Vector3.Lerp(startPosition, endPosition, t);
        StartCoroutine(MoveToPosition(targetPos, 0.3f));
    }

    private IEnumerator WalkBoolResetTimer()
    {
        yield return new WaitForSeconds(walkDuration);
        anim.SetBool("IsWalkB", false);
    }

    private IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        Vector3 initialPos = player.transform.position;
        float elapsed = 0f;

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
        anim.SetTrigger("IsWin");
        base.MissionClear();
    }

    protected override IEnumerator UpdateProgressBar()
    {
        yield return base.UpdateProgressBar();
        anim.SetBool("IsSliding", true);

    }

    protected override IEnumerator FailEnding()
    {

        prograssbar.fillAmount = 1f;
        yield return new WaitForSeconds(finishTime);
        failAction.SetActive(true);
        stageManager.Life--;
        yield return new WaitForSeconds(endingTime);

        Debug.Log("�������̵�");
        anim.SetBool("IsSliding", false);
        stageManager.isStagenext = true;
    }
}
