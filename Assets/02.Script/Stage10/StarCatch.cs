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




        // 레벨에 따라 속도 계산 (레벨이 높을수록 빠르게 → duration은 짧게)
        int level = Mathf.Clamp(stage.level, 1, 4); // 1~5 보정
        float duration = Mathf.Lerp(1f, 0.5f, (level - 1) / 3f); // 1레벨일 때 1초, 5레벨일 때 0.3초

        LeanTween.value(gameObject, 0f, 100f, duration)
            .setOnUpdate(val => slider.value = val)
            .setLoopPingPong();
    }


    void StopSlider()
    {
        LeanTween.cancel(gameObject);
        isMoving = false;

        // 슬라이더 현재 값(0~100)을 UI X좌표로 환산
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
