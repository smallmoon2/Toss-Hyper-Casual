using UnityEngine;

public class test_onE : MonoBehaviour
{
    public GameObject target;      // 켜고 끌 대상 오브젝트
    public float delay = 0.5f;     // 꺼진 후 다시 켜지는 시간

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target.SetActive(true);            // 다시 켜기
        // 끄기
        }
        if (Input.GetMouseButtonDown(1))
        {
          // 다시 켜기
            target.SetActive(false);           // 끄기
        }
    }

}
