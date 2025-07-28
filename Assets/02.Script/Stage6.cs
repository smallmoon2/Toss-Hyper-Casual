using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stage6 : StageBase
{
    private int touchCount = 0;
    private bool isfinish = false;
    public int maxTouches = 15;
    public GameObject enemyMush;

    protected override void OnEnable()
    {
        isfinish = false;
        finishTime = 1f;
        touchCount = 0;
        maxTouches = 15;
        playTime = 5f;
        endingTime = 2f;

        base.OnEnable();

        // �� ���� �ڵ� �̵� ����
        StartCoroutine(MoveEnemyToEnd());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) ||
            (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (!isfinish)
            {
                HandleTouch();
            }
        }
    }

    private void HandleTouch()
    {
        if (touchCount >= maxTouches) return;

        touchCount++;
        float t = touchCount / (float)maxTouches;
        float easedT = t * t * t; // Ease-in

        Vector3 targetPos = Vector3.Lerp(startPosition, endPosition, t);
        Vector3 targetScale = Vector3.Lerp(player.transform.localScale, endPos.transform.localScale, easedT);
        StartCoroutine(MoveToPosition(targetPos, targetScale, 0.3f));
    }

    private IEnumerator MoveToPosition(Vector3 targetPos, Vector3 targetScale, float duration)
    {
        Vector3 initialPos = player.transform.position;
        Vector3 initialScale = player.transform.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            player.transform.position = Vector3.Lerp(initialPos, targetPos, t);
            player.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        player.transform.position = targetPos;
        player.transform.localScale = targetScale;

        if (touchCount >= maxTouches &&
            Vector3.Distance(targetPos, endPosition) < 0.01f &&
            Vector3.Distance(player.transform.localScale, endPos.transform.localScale) < 0.01f)
        {
            MissionClear();
        }
    }

    private IEnumerator MoveEnemyToEnd()
    {
        Vector3 startPos = enemyMush.transform.position;
        Vector3 startScale = enemyMush.transform.localScale;
        Vector3 endPosEnemy = endPos.transform.position;
        Vector3 endScale = endPos.transform.localScale;

        float elapsed = 0f;

        while (elapsed < playTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / playTime);
            float easedT = t * t * t; // Ease-in

            enemyMush.transform.position = Vector3.Lerp(startPos, endPosEnemy, t);
            enemyMush.transform.localScale = Vector3.Lerp(startScale, endScale, easedT);

            yield return null;
        }

        // ����
        enemyMush.transform.position = endPosEnemy;
        enemyMush.transform.localScale = endScale;
        isfinish = true;

    }

    protected override void MissionClear()
    {
        base.MissionClear();
    }

    protected override IEnumerator UpdateProgressBar()
    {
        yield return base.UpdateProgressBar();
    }
}
