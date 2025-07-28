using UnityEngine;

public class test_Script : MonoBehaviour
{

    public GameObject body;
    public float rotationSpeed = 300f; // 회전 속도

    void Update()
    {
        if (Input.GetMouseButton(0)) // 꾹 누르고 있는 동안
        {
            Vector3 touchPos = Input.mousePosition;
            float screenMidX = Screen.width / 2f;

            if (touchPos.x < screenMidX)
            {
                // 왼쪽 화면 꾹 누름 → 반시계 회전
                RotateBody(-rotationSpeed);
            }
            else
            {
                // 오른쪽 화면 꾹 누름 → 시계 회전
                RotateBody(rotationSpeed);
            }
        }
    }

    void RotateBody(float amount)
    {
        if (body != null)
        {
            body.transform.Rotate(0f, 0f, amount * Time.deltaTime);
        }
    }
}
