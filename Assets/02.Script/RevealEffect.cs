using System.Collections;
using UnityEngine;

public class RevealEffect : MonoBehaviour
{
    public Material revealMaterial;       // 원본 머티리얼 (Inspector에 지정)
    private Material runtimeMaterial;     // 런타임 인스턴스

    public float revealTime = 1f;

    void Start()
    {
        // 머티리얼 복사본 생성
        runtimeMaterial = new Material(revealMaterial);

        // Renderer에 복사본 머티리얼 적용
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = runtimeMaterial;
        }

        StartCoroutine(Reveal());
    }

    IEnumerator Reveal()
    {
        float t = 0f;
        while (t < revealTime)
        {
            t += Time.deltaTime;
            float value = Mathf.Clamp01(t / revealTime); // ← 왼쪽 → 오른쪽
            runtimeMaterial.SetFloat("_Cutoff", value);
            yield return null;
        }

        runtimeMaterial.SetFloat("_Cutoff", 1f); // 마지막 값 보장
    }
}
