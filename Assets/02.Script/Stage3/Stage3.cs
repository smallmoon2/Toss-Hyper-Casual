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
        maxPlayTime = 3f;  // 최대 시간
        minPlayTime = 1.5f;  // 최소 시간
        base.OnEnable();

        eggStage = true;
        Stage3_Clear.GameReset();
        

        isDragging = false;
        point = 1;
        dragStartPos = Vector3.zero;

        if (Stage3_Clear.isOtherVersion)
        {
            if (point == 1)
            {
                timeclear = false;
            }
            else
            {
                timeclear = true;
            }
        }
        else
        {
            if (point == 1)
            {
                timeclear = true;
            }
            else
            {
                timeclear = false;
            }
        }

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
        if (!PauseManager.Instance || !PauseManager.Instance.IsPaused)
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
                    if (player != null)
                        if (point < 2)
                        {
                            point++;
                        }

                }
                else if (deltaX < -detectThreshold)
                {
                    Debug.Log("왼쪽");
                    if (player != null)
                    {
                        if (point > 0)
                        {
                            point--;
                        }
                    }
                }
                player.transform.position = chearPos[point].position;
                SoundManager.Instance.Play("Hit");


                isDragging = false;

                if (Stage3_Clear.isOtherVersion)
                {
                    if (point == 1)
                    {
                        timeclear = false;
                    }
                    else
                    {
                        timeclear = true;
                    }
                }
                else
                {
                    if (point == 1)
                    {
                        timeclear = true;
                    }
                    else
                    {
                        timeclear = false;
                    }
                }

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