using UnityEngine;

public class Stage8 : StageBase
{
    public Rigidbody2D bodyRb;
    public float torqueForce = 10f;

    private bool holdingLeft = false;
    private bool holdingRight = false;

    private bool stage8Next = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        finishTime = 1;
        stage8Next = false;
        playTime = 7;
        timeclear = true;

        
        float direction = Random.value < 0.5f ? -1f : 1f;
        bodyRb.AddTorque(1 * direction, ForceMode2D.Force);
    }

    void Update()
    {
        Debug.Log(timeclear);
        if (Input.GetMouseButton(0)&& !stage8Next)
        {

            Vector3 touchPos = Input.mousePosition;
            float screenMidX = Screen.width / 2f;

            if (touchPos.x < screenMidX)
            {
                holdingLeft = true;
                holdingRight = false;
            }
            else
            {
                holdingRight = true;
                holdingLeft = false;
            }
        }
        else
        {
            holdingLeft = false;
            holdingRight = false;
        }

        if (prograssbar.fillAmount == 0f)
        {
            stage8Next = true;
        }
        CheckFallOver();
    }

    void FixedUpdate()
    {
        if (bodyRb == null) return;

        if (holdingLeft)
        {
            bodyRb.AddTorque(torqueForce, ForceMode2D.Force); // 반시계
        }
        else if (holdingRight)
        {
            bodyRb.AddTorque(-torqueForce, ForceMode2D.Force); // 시계
        }
    }

    void CheckFallOver()
    {
        if (bodyRb == null) return;

        float zRotation = bodyRb.rotation;


        if ((zRotation < -45f || zRotation > 45f) && !stage8Next)
        {
            StartCoroutine(FailEnding());
            Debug.Log("넘어짐");
            stage8Next = true;
        }

        // Unity의 Rigidbody2D.rotation은 -180 ~ 180 도
    }
}
