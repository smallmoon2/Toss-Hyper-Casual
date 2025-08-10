using UnityEngine;

public class HandHelp : MonoBehaviour
{
    public int touchcount = 1; // 기본 1회 터치 시 사라짐
    private int currentTouches = 0;

    void Update()
    {
        // 모바일 터치 입력 감지
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            currentTouches++;
            if (currentTouches >= touchcount)
            {
                gameObject.SetActive(false);
            }
        }

        // 에디터/PC용 마우스 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            currentTouches++;
            if (currentTouches >= touchcount)
            {
                gameObject.SetActive(false);
            }
        }
    }
}