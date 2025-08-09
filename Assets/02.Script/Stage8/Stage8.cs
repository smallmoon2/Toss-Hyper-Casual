using UnityEngine;
using System.Collections;

public class Stage8 : StageBase
{
    public Rigidbody2D bodyRb;
    public float torqueForce = 10f;

    private float maxSize = 1.0f;
    private float minSize = 0.5f;

    private bool holdingLeft = false;
    private bool holdingRight = false;
    public GameObject Startcylinder;
    private bool stage8Next = false;
    public MaskController maskController;
    private Vector3 initialCylinderPosition;
    private Quaternion initialCylinderRotation;

    private void Awake()
    {
        if (Startcylinder != null)
        {
            initialCylinderPosition = Startcylinder.transform.position;
            initialCylinderRotation = Startcylinder.transform.rotation;
        }
    }

    protected override void OnEnable()
    {
        maxPlayTime = 5f;
        minPlayTime = 5f;

        base.OnEnable();
        maskController.ActivateMaskChild(0);
        finishTime = 1f;
        playTime = 7f;
        timeclear = true;

        stage8Next = true;

        if (Startcylinder != null)
        {
            Startcylinder.transform.position = initialCylinderPosition;
            Startcylinder.transform.rotation = initialCylinderRotation;
        }

        if (bodyRb != null)
        {

            int clampedLevel = Mathf.Clamp(stageManager.StageLevel, 1, 4);
            float t = (clampedLevel - 1f) / 3f;  // 0 ~ 1
            float size = Mathf.Lerp(maxSize, minSize, t);
            bodyRb.transform.localScale = new Vector3(size, size, 1f);

            bodyRb.linearVelocity = Vector2.zero;
            bodyRb.angularVelocity = 0f;
            bodyRb.rotation = 0f;

            float direction = Random.value < 0.5f ? -1f : 1f;
            bodyRb.AddTorque(1 * direction, ForceMode2D.Force);
        }

        StartCoroutine(AllowFallOverCheckNextFrame());
    }


    private IEnumerator AllowFallOverCheckNextFrame()
    {
        yield return null;
        stage8Next = false;
    }

    private void Update()
    {
        Debug.Log(bodyRb.rotation);

        if (Input.GetMouseButton(0) && !stage8Next)
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

    private void FixedUpdate()
    {
        if (bodyRb == null) return;

        if (holdingLeft)
        {
            bodyRb.AddTorque(torqueForce, ForceMode2D.Force);
        }
        else if (holdingRight)
        {
            bodyRb.AddTorque(-torqueForce, ForceMode2D.Force); 
        }
    }

    protected override IEnumerator ClearEnding()
    {
        isClear = true;
        stageManager.scoreNum += 100;
        prograssbar.fillAmount = 1f;

        yield return new WaitForSeconds(0);
        clearAction.SetActive(true);
        Setreset();

        yield return new WaitForSeconds(endingTime);
        stageManager.isStagenext = true;
    }

    private void CheckFallOver()
    {
        if (bodyRb == null) return;

        float zRotation = bodyRb.rotation;

        if ((zRotation < -45f || zRotation > 45f) && !stage8Next)
        {
            StartCoroutine(FailEnding());
            SoundManager.Instance.Play("FowardDown");

            maskController.ActivateMaskChild(3);
            Debug.Log(zRotation);
            stage8Next = true;
        }
    }
}
