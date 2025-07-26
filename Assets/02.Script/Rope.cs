using UnityEngine;

public class Rope : MonoBehaviour
{
    public float rotationSpeed = 180f; // �ʴ� ȸ�� �ӵ�
    public Transform pivot; // ȸ�� �߽��� �Ǵ� Transform


    private void OnEnable()
    {
        pivot.eulerAngles = new Vector3(30f, 0f, 0f);
    }

    void Update()
    {
        if (pivot != null)
        {
            // Pivot�� �߽����� Z�� ȸ��
            pivot.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
    }
}