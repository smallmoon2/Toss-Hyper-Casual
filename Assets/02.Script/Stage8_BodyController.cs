using UnityEngine;

public class Stage8_BodyController : MonoBehaviour
{
    public GameObject bodyControllObject;

    void Update()
    {
        if (bodyControllObject != null)
        {
            // ȸ�� �� ����ȭ (Z�ุ ȸ���ϴ� 2D ȯ�� ���� ��)
            transform.rotation = bodyControllObject.transform.rotation;
        }
    }
}
