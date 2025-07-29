using UnityEngine;

public class Stage9 : StageBase
{
    public Stage9_BaseBall baseBall;
    public Transform bone;

    public float num1 = 0f;
    public float num2 = 0f;

    private bool stage9Next;
    private Vector3 lastMousePos;
    private bool isDragging = false;
    public float rotationSpeed = 100f;

    private float currentZ = 0f; // �������� ������ ȸ����

    void Start()
    {
        playTime = 5f;
        finishTime = 2f;
        base.OnEnable();

        currentZ = bone.localEulerAngles.z;
    }

    void Update()
    {
        HandleMouseDragRotation();

        if (baseBall.isClear && !stage9Next)
        {
            Debug.Log("���� ó��");
            StartCoroutine(ClearEnding());
            stage9Next = true;
        }
    }

    private void HandleMouseDragRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePos = Input.mousePosition;
            return; // ù ������ ȸ�� ����
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 currentMousePos = Input.mousePosition;
            float deltaX = currentMousePos.x - lastMousePos.x;

            float rotationAmount = deltaX * rotationSpeed * Time.deltaTime;

            currentZ -= rotationAmount;
            currentZ = Mathf.Clamp(currentZ, num1, num2);

            bone.localRotation = Quaternion.Euler(0f, 0f, currentZ);

            lastMousePos = currentMousePos;
        }
    }

}
