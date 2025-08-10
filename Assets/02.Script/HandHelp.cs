using UnityEngine;

public class HandHelp : MonoBehaviour
{
    public int touchcount = 1; // �⺻ 1ȸ ��ġ �� �����
    private int currentTouches = 0;

    void Update()
    {
        // ����� ��ġ �Է� ����
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            currentTouches++;
            if (currentTouches >= touchcount)
            {
                gameObject.SetActive(false);
            }
        }

        // ������/PC�� ���콺 Ŭ�� ����
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