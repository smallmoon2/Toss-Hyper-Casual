using System.Collections;
using UnityEngine;

public class Stage2_Clear : StageClearBase
{

    public Transform targetObject;
    public Transform startPos;
    public Transform endPos;
    public float moveDuration = 1.5f;



    protected override IEnumerator Clear()
    {
        if (targetObject == null || startPos == null || endPos == null)
        {
            Debug.LogWarning("Stage2_Clear: 필수 Transform이 비어 있음");
            yield break;
        }

        targetObject.position = startPos.position;

        Animator anim = targetObject.GetComponent<Animator>();
        if (anim) anim.SetBool("IsRun", true);
        SoundManager.Instance.PlayLoop("Walk");
        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            targetObject.position = Vector3.Lerp(startPos.position, endPos.position, t);
            yield return null;
        }

        targetObject.position = endPos.position;
        SoundManager.Instance.Stop();

        SoundManager.Instance.Play("Clear_2_2");
        if (anim) anim.SetTrigger("IsWin");
    }

    protected override IEnumerator Fail()
    {
        if (targetObject == null || startPos == null || endPos == null)
        {
            Debug.LogWarning("Stage2_Clear: 필수 Transform이 비어 있음");
            yield break;
        }

        targetObject.position = startPos.position;

        Animator anim = targetObject.GetComponent<Animator>();
        if (anim) anim.SetBool("IsRun", true);

        float stopRatio = 0.5f;
        float stopTime = moveDuration * stopRatio;
        float elapsed = 0f;

        while (elapsed < stopTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            targetObject.position = Vector3.Lerp(startPos.position, endPos.position, t);
            yield return null;
        }

        targetObject.position = Vector3.Lerp(startPos.position, endPos.position, stopRatio);

        if (anim)
        {
            anim.SetBool("IsRun", false);
            anim.SetBool("IsFowardDown", true);
        }
    }
}


