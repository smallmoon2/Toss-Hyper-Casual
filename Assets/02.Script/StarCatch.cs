using UnityEngine;
using UnityEngine.UI;

public class StarCatch : MonoBehaviour
{
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

        float randX = 540f;
        float randWidth = 150f;
        rangeBox.anchoredPosition = new Vector2(randX, 0);

        rangeBox.sizeDelta = new Vector2(randWidth, rangeBox.sizeDelta.y);

        LeanTween.value(gameObject, 0f, 100f, 1f)
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

        if (sliderUIPosX >= 44 && sliderUIPosX <= 55)
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
