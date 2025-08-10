using System.Collections;
using UnityEngine;

public class RevealEffect : MonoBehaviour
{

    public ScoreStage scoreStage;
    public Material revealMaterial;       // 원본 머티리얼 (Inspector에 지정)
    private Material runtimeMaterial;     // 런타임 인스턴스

    private float revealTime = 0.6f;



    public bool isStart;

    void OnEnable()
    {
        if (isStart)
        {
            Debug.Log("안그림");
            return;
        }
        // 머티리얼 복사본 생성
        runtimeMaterial = new Material(revealMaterial);

        // Renderer에 복사본 머티리얼 적용
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = runtimeMaterial;
        }

        StartCoroutine(Reveal());

        isStart = true;
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

    public void ResetStartFlag()
    {
        isStart = false;

    }
}
