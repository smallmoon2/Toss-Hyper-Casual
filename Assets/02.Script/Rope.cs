using UnityEngine;

public class Rope : MonoBehaviour
{
    public Stage4 Stage4;
    public float rotationSpeed = 180f; // �ʴ� ȸ�� �ӵ�
    public Transform pivot; // ȸ�� �߽��� �Ǵ� Transform


    private void OnEnable()
    {
        pivot.eulerAngles = new Vector3(30f, 0f, 0f);
    }

    void Update()
    {
        if (pivot != null && !Stage4.stage4Next)
        {
            // Pivot�� �߽����� Z�� ȸ��
            pivot.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
    }
}