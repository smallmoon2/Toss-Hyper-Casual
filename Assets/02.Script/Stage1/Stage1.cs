using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stage1 : StageBase
{
    public Door_Screen door;
    private int touchCount = 0;
    public int maxTouches = 10;
    private Animator anim;


    private bool isFinshed = false;
    protected override void OnEnable()
    {
        finishTime = 1f;
        touchCount = 0;
        maxTouches = 10;
        isFinshed = false;
        playTime = 5f;
        endingTime = 2f;
        base.OnEnable();
        door.OpenDoor();
        anim = player.GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.Log(isFinshed);
        if (Input.GetMouseButtonDown(0) ||
            (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (!isFinshed)
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
        Vector3 targetPos = Vector3.Lerp(startPosition, endPosition, t);
        StartCoroutine(MoveToPosition(targetPos, 0.3f));
    }

    private IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        Vector3 initialPos = player.transform.position;
        float elapsed = 0f;

        //  �ִϸ��̼� ����: IsRun = true
        
        if (anim != null)
        {
            anim.SetBool("IsRun", true);
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
        if (anim != null)
        {
            anim.SetBool("IsRun", false);
        }
        door.CloseDoor();           // ���� �� �ݱ�
        base.MissionClear();        // �⺻ �̼� Ŭ���� ó�� ����
    }
    protected override IEnumerator UpdateProgressBar()
    {
 
        // ���� �� �ݱ�
        yield return base.UpdateProgressBar();
        door.CloseDoor();
        if (anim != null)
        {
            anim.SetTrigger("IsFowardDown");
        }
        isFinshed = true;
    }
}