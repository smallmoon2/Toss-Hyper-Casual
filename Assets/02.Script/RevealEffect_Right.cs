using System.Collections;
using UnityEngine;

public class RevealEffect_Right : MonoBehaviour
{
    public Material revealMaterial;
    private Material runtimeMaterial;
    public float revealTime = 1f;

    void Start()
    {
        // ���� ��Ƽ������ �����ؼ� ��� (�ν��Ͻ�)
        runtimeMaterial = new Material(revealMaterial);
        GetComponent<SpriteRenderer>().material = runtimeMaterial;

        StartCoroutine(Reveal());
    }

    IEnumerator Reveal()
    {
        yield return new WaitForSeconds(0.5f);
        float t = 0f;
        while (t < revealTime)
        {
            t += Time.deltaTime;
            float value = Mathf.Clamp01(1f - (t / revealTime));
            runtimeMaterial.SetFloat("_Cutoff", value);
            yield return null;
        }

        runtimeMaterial.SetFloat("_Cutoff", 0f);
    }
}
