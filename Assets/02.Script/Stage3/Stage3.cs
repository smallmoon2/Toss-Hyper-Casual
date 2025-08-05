using System.Drawing;
using UnityEngine;

public class Stage3 : StageBase
{
    public Stage3_Clear Stage3_Clear;
    private Vector3 dragStartPos;
    private bool isDragging = false;
    public float detectThreshold = 0.5f; // 감지 민감도

    public Transform[] chearPos;
    private int point = 1;


    protected override void OnEnable()
    {
        base.OnEnable();
        Stage3_Clear.GameReset();
        isDragging = false;
        point = 1;
        dragStartPos = Vector3.zero;
    }

    private void Update()
    {
        if (!isFinshed)
        {
            HandleMouseDrag();
        }

    }

    void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = GetWorldPoint();
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Vector3 dragEndPos = GetWorldPoint();
            float deltaX = dragEndPos.x - dragStartPos.x;

            if (deltaX > detectThreshold)
            {
                Debug.Log("오른쪽");
                if (player != null )
                    if (point < 2)
                    {
                        point++;
                    }
                    
            }
            else if (deltaX < -detectThreshold)
            {
                Debug.Log("왼쪽");
                if (player != null )
                {
                    if (point > 0)
                    {
                        point--;
                    }
                }
            }
            player.transform.position = chearPos[point].position;
            isDragging = false;
            if (point == 1)
            {
                timeclear = false;
            }
            else
            {
                timeclear = true;
            }
        }
    }


    Vector3 GetWorldPoint()
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10f; // 카메라 거리
        return Camera.main.ScreenToWorldPoint(screenPoint);
    }
}