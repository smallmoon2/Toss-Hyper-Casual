using UnityEngine;

public class Rope : MonoBehaviour
{
    public Stage4 Stage4;
    private float rotationSpeed = 180f; // �ʴ� ȸ�� �ӵ�
    private float MaxrotationSpeed = 330f;
    private float MinrotationSpeed = 180f;
    public Transform pivot; // ȸ�� �߽��� �Ǵ� Transform


    private void OnEnable()
    {
        rotationSpeed = MinrotationSpeed + (Stage4.level * (MaxrotationSpeed - MinrotationSpeed) / 3f);
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