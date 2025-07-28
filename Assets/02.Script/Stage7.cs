using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stage7 : StageBase
{
    private int touchCount = 0;
    public int maxTouches = 10;
    private float touchtime = 0.3f;

    private float lastTouchTime = -10f;
    private bool isFailing = false;

    protected override void OnEnable()
    {
        finishTime = 1f;
        touchCount = 0;
        maxTouches = 10;
        playTime = 5f;
        endingTime = 2f;
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
            Debug.Log("빠르게 두 번 터치됨 → 실패");
            StartCoroutine(FailEnding());
            isFailing = true;
            return;
        }

        lastTouchTime = now;

        touchCount++;
        float t = touchCount / (float)maxTouches;
        Vector3 targetPos = Vector3.Lerp(startPosition, endPosition, t);
        StartCoroutine(MoveToPosition(targetPos, 0.3f));
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
        base.MissionClear(); // 기본 미션 클리어 처리 실행
    }

    protected override IEnumerator UpdateProgressBar()
    {
        yield return base.UpdateProgressBar(); // 기본 진행바 업데이트 실행
    }
}
