using System.Collections;
using UnityEngine;

public class RevealEffect : MonoBehaviour
{
    public Material revealMaterial;       // ���� ��Ƽ���� (Inspector�� ����)
    private Material runtimeMaterial;     // ��Ÿ�� �ν��Ͻ�

    public float revealTime = 1f;

    void Start()
    {
        // ��Ƽ���� ���纻 ����
        runtimeMaterial = new Material(revealMaterial);

        // Renderer�� ���纻 ��Ƽ���� ����
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
            float value = Mathf.Clamp01(t / revealTime); // �� ���� �� ������
            runtimeMaterial.SetFloat("_Cutoff", value);
            yield return null;
        }

        runtimeMaterial.SetFloat("_Cutoff", 1f); // ������ �� ����
    }
}
