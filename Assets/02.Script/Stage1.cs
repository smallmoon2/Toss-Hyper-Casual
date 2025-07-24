using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stage1 : StageBase
{
    public Door_Screen door;
    private int touchCount = 0;
    public int maxTouches = 10;

    protected override void OnEnable()
    {
        touchCount = 0;
        maxTouches = 10;
        playTime = 5f;
        endingTime = 2f;
        base.OnEnable();
        door.OpenDoor();
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
        door.CloseDoor();           // 먼저 문 닫기
        base.MissionClear();        // 기본 미션 클리어 처리 실행
    }
    protected override IEnumerator UpdateProgressBar()
    {
               // 먼저 문 닫기
        yield return base.UpdateProgressBar();
        door.CloseDoor();
    }
}