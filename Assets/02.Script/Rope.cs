using UnityEngine;

public class Rope : MonoBehaviour
{
    public float rotationSpeed = 180f; // 초당 회전 속도
    public Transform pivot; // 회전 중심이 되는 Transform


    private void OnEnable()
    {
        pivot.eulerAngles = new Vector3(30f, 0f, 0f);
    }

    void Update()
    {
        if (pivot != null)
        {
            // Pivot을 중심으로 Z축 회전
            pivot.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
    }
}