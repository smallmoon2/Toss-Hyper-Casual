using UnityEngine;

public class test_Script : MonoBehaviour
{

    public GameObject body;
    public float rotationSpeed = 300f; // ȸ�� �ӵ�

    void Update()
    {
        if (Input.GetMouseButton(0)) // �� ������ �ִ� ����
        {
            Vector3 touchPos = Input.mousePosition;
            float screenMidX = Screen.width / 2f;

            if (touchPos.x < screenMidX)
            {
                // ���� ȭ�� �� ���� �� �ݽð� ȸ��
                RotateBody(-rotationSpeed);
            }
            else
            {
                // ������ ȭ�� �� ���� �� �ð� ȸ��
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
