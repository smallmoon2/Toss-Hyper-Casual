using UnityEngine;

public class test_onE : MonoBehaviour
{
    public GameObject target;      // �Ѱ� �� ��� ������Ʈ
    public float delay = 0.5f;     // ���� �� �ٽ� ������ �ð�

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target.SetActive(true);            // �ٽ� �ѱ�
        // ����
        }
        if (Input.GetMouseButtonDown(1))
        {
          // �ٽ� �ѱ�
            target.SetActive(false);           // ����
        }
    }

}
