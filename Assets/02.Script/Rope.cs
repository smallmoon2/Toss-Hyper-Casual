using UnityEngine;

public class Rope : MonoBehaviour
{
    public Stage4 Stage4;
    public float rotationSpeed = 180f; // 초당 회전 속도
    public Transform pivot; // 회전 중심이 되는 Transform


    private void OnEnable()
    {
        pivot.eulerAngles = new Vector3(30f, 0f, 0f);
    }

    void Update()
    {
        if (pivot != null && !Stage4.stage4Next)
        {
            // Pivot을 중심으로 Z축 회전
            pivot.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
    }
}