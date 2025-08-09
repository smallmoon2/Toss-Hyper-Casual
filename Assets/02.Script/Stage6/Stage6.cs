using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stage6 : StageBase
{
    public Stage6_Clear Stage6_Clear;
    private int touchCount = 0;
    private bool isfinish = false;
    public GameObject enemyMush_StartPos;
    public int maxTouches = 15;
    public GameObject enemyMush;
    private Animator anim;
    private Animator enemyMushAnim;
    protected override void OnEnable()
    {
        maxPlayTime = 6f;  // 최대 시간
        minPlayTime = 4f;  // 최소 시간
        
        isfinish = false;
        finishTime = 0.2f;
        touchCount = 0;
        maxTouches = 15;
        playTime = 5f;
        endingTime = 2f;
        enemyMush.transform.position = enemyMush_StartPos.transform.position;

        player.transform.localScale = Vector3.one;
        enemyMush.transform.localScale = Vector3.one;

        enemyMushAnim = enemyMush.GetComponent<Animator>();
        anim = player.GetComponent<Animator>();
        base.OnEnable();
        Stage6_Clear.GameReset();
        // 적 버섯 자동 이동 시작
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

        if (anim != null)
        {
            SoundManager.Instance.PlayLoop("Walk");
            anim.SetBool("IsRun", true);
        }

        while (elapsed < duration && (!isfinish))
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
            Vector3 pos = player.transform.position;
            pos.z -= 1f;
            player.transform.position = pos;
            SoundManager.Instance.Stop();
            MissionClear();
        }
    }

    private IEnumerator MoveEnemyToEnd()
    {
        Vector3 startPos = enemyMush.transform.position;
        Vector3 startScale = enemyMush.transform.localScale;

        Vector3 endPosEnemy = endPos.transform.position;
        endPosEnemy.z -= 0.002f; // 

        Vector3 endScale = endPos.transform.localScale;

        float elapsed = 0f;

        if (enemyMushAnim != null)
        {
            enemyMushAnim.SetBool("IsRun", true);
        }

        while (elapsed < playTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / playTime);
            float easedT = t * t * t; // Ease-in

            enemyMush.transform.position = Vector3.Lerp(startPos, endPosEnemy, t);
            enemyMush.transform.localScale = Vector3.Lerp(startScale, endScale, easedT);

            yield return null;
        }

        // 보정
        endPosEnemy.z -= 1.1f;
        enemyMush.transform.position = endPosEnemy;
        enemyMush.transform.localScale = endScale;
        isfinish = true;

        if (enemyMushAnim != null)
        {
            enemyMushAnim.SetTrigger("IsWin");
        }
    }


    protected override void MissionClear()
    {
        if (anim != null)
        {
            SoundManager.Instance.Play("Clear_2_2");

            anim.SetBool("IsRun", false);
        }

        base.MissionClear();
    }

    protected override IEnumerator UpdateProgressBar()
    {
        yield return base.UpdateProgressBar();
    }
}