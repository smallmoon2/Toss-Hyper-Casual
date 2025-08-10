using System.Collections;
using UnityEngine;

public class RevealEffect : MonoBehaviour
{

    public ScoreStage scoreStage;
    public Material revealMaterial;       // ���� ��Ƽ���� (Inspector�� ����)
    private Material runtimeMaterial;     // ��Ÿ�� �ν��Ͻ�

    private float revealTime = 0.6f;



    public bool isStart;

    void OnEnable()
    {
        if (isStart)
        {
            Debug.Log("�ȱ׸�");
            return;
        }
        // ��Ƽ���� ���纻 ����
        runtimeMaterial = new Material(revealMaterial);

        // Renderer�� ���纻 ��Ƽ���� ����
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
            float value = Mathf.Clamp01(t / revealTime); // �� ���� �� ������
            runtimeMaterial.SetFloat("_Cutoff", value);
            yield return null;
        }

        runtimeMaterial.SetFloat("_Cutoff", 1f); // ������ �� ����
    }

    public void ResetStartFlag()
    {
        isStart = false;

    }
}
