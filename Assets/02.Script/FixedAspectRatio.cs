using UnityEngine;

public class FixedAspectRatio : MonoBehaviour
{
    public Vector2 targetAspectRatio = new Vector2(9, 16);
    private Camera cam;
    private int lastScreenWidth;
    private int lastScreenHeight;

    void Start()
    {
        cam = Camera.main;
        if (!cam) return;

        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
        ApplyAspect();
    }

    void Update()
    {
        // 화면 크기가 변했는지 체크
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            ApplyAspect();
        }
    }

    void ApplyAspect()
    {
        float targetAspect = targetAspectRatio.x / targetAspectRatio.y;
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1f)
        {
            Rect rect = cam.rect;
            rect.width = 1f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1f - scaleHeight) / 2f;
            cam.rect = rect;
        }
        else
        {
            float scaleWidth = 1f / scaleHeight;
            Rect rect = cam.rect;
            rect.width = scaleWidth;
            rect.height = 1f;
            rect.x = (1f - scaleWidth) / 2f;
            rect.y = 0;
            cam.rect = rect;
        }
    }
}
