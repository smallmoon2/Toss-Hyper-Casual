using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour
{
    private RectTransform rect;

    [SerializeField] private float scaleUpTime = 0.7f;    // Ŀ���� �ð�
    [SerializeField] private float holdTime = 0.7f;       // ���� �ð�
    [SerializeField] private float scaleDownTime = 0.7f;  // �پ��� �ð�
    [SerializeField] private float targetScale = 16f;     // ���� Ȯ�� ����

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    /// <summary>
    /// RectTransform�� scale�� targetScale���� scaleUpTime ���� Ű���
    /// holdTime �����ٰ� �ٽ� 0���� scaleDownTime ���� ���̴� ����
    /// </summary>
    public void PlayScaleEffect()
    {
        StartCoroutine(ScaleEffectRoutine());
    }

    private IEnumerator ScaleEffectRoutine()
    {
        // ���۰�
        Vector3 startScale = rect.localScale;
        Vector3 target = Vector3.one * targetScale;

        // scaleUpTime ���� targetScale�� Ŀ��
        float t = 0f;
        while (t < scaleUpTime)
        {
            t += Time.deltaTime;
            rect.localScale = Vector3.Lerp(startScale, target, t / scaleUpTime);
            yield return null;
        }

        // ��� ��� (holdTime)
        yield return new WaitForSeconds(holdTime);

        // scaleDownTime ���� �ٽ� 0���� �پ��
        t = 0f;
        Vector3 endScale = Vector3.zero;
        Vector3 currentScale = rect.localScale;
        while (t < scaleDownTime)
        {
            t += Time.deltaTime;
            rect.localScale = Vector3.Lerp(currentScale, endScale, t / scaleDownTime);
            yield return null;
        }

        // ������ ����
        rect.localScale = endScale;
    }
}
