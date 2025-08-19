using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour
{
    private RectTransform rect;

    [SerializeField] private float scaleUpTime = 0.7f;    // 커지는 시간
    [SerializeField] private float holdTime = 0.7f;       // 유지 시간
    [SerializeField] private float scaleDownTime = 0.7f;  // 줄어드는 시간
    [SerializeField] private float targetScale = 16f;     // 최종 확대 배율

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    /// <summary>
    /// RectTransform의 scale을 targetScale까지 scaleUpTime 동안 키우고
    /// holdTime 쉬었다가 다시 0까지 scaleDownTime 동안 줄이는 연출
    /// </summary>
    public void PlayScaleEffect()
    {
        StartCoroutine(ScaleEffectRoutine());
    }

    private IEnumerator ScaleEffectRoutine()
    {
        // 시작값
        Vector3 startScale = rect.localScale;
        Vector3 target = Vector3.one * targetScale;

        // scaleUpTime 동안 targetScale로 커짐
        float t = 0f;
        while (t < scaleUpTime)
        {
            t += Time.deltaTime;
            rect.localScale = Vector3.Lerp(startScale, target, t / scaleUpTime);
            yield return null;
        }

        // 잠깐 대기 (holdTime)
        yield return new WaitForSeconds(holdTime);

        // scaleDownTime 동안 다시 0으로 줄어듦
        t = 0f;
        Vector3 endScale = Vector3.zero;
        Vector3 currentScale = rect.localScale;
        while (t < scaleDownTime)
        {
            t += Time.deltaTime;
            rect.localScale = Vector3.Lerp(currentScale, endScale, t / scaleDownTime);
            yield return null;
        }

        // 최종값 보정
        rect.localScale = endScale;
    }
}
