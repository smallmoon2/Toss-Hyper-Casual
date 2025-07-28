using UnityEngine;

public class Stage8_BodyController : MonoBehaviour
{
    public GameObject bodyControllObject;

    void Update()
    {
        if (bodyControllObject != null)
        {
            // 회전 값 동기화 (Z축만 회전하는 2D 환경 가정 시)
            transform.rotation = bodyControllObject.transform.rotation;
        }
    }
}
