using UnityEngine;
using UnityEngine.UI;

public class StarCatch : MonoBehaviour
{
    public StageBase stage;
    public Slider slider;
    public RectTransform rangeBox;

    private bool isMoving = false;

    public enum isStarCatch { Play, Fail, Clear };

    public isStarCatch isCatch;

    void OnEnable()
    {

        isCatch = isStarCatch.Play;
        isMoving = false;
        StartSlider();
    }

    void Update()
    {
        if (isMoving && Input.GetMouseButtonDown(0))
        {
            StopSlider();
        }
    }

    void StartSlider()
    {
        isMoving = true;




        // ������ ���� �ӵ� ��� (������ �������� ������ �� duration�� ª��)
        int level = Mathf.Clamp(stage.level, 1, 4); // 1~5 ����
        float duration = Mathf.Lerp(1f, 0.5f, (level - 1) / 3f); // 1������ �� 1��, 5������ �� 0.3��

        LeanTween.value(gameObject, 0f, 100f, duration)
            .setOnUpdate(val => slider.value = val)
            .setLoopPingPong();
    }


    void StopSlider()
    {
        LeanTween.cancel(gameObject);
        isMoving = false;

        // �����̴� ���� ��(0~100)�� UI X��ǥ�� ȯ��
        float sliderUIPosX = (slider.value);

        Debug.Log(sliderUIPosX);

        if (sliderUIPosX >= 45 && sliderUIPosX <= 57)
        {
            Debug.Log(" Success!");
            isCatch = isStarCatch.Clear;
        }
        else
        {
            Debug.Log(" Failed!");
            isCatch = isStarCatch.Fail;
        }
    }

}
