using UnityEngine;

public class Stage9 : StageBase
{
    public Stage9_BaseBall baseBall;
    public Transform bone;
    public Stage9_Clear Stage9_Clear;
    public float num1 = 0f;
    public float num2 = 0f;
    public GameObject Baseball;
    
    private bool stage9Next;
    private Vector3 lastMousePos;
    private bool isDragging = false;
    public float rotationSpeed = 100f;

    private float currentZ = 0f; // �������� ������ ȸ����

    protected override void OnEnable()
    {
        maxPlayTime = 3f;  // �ִ� �ð�
        minPlayTime = 1.5f;  // �ּ� �ð�
        playTime = 4f;
        finishTime = 0.2f;
        base.OnEnable();
        Stage9_Clear.GameReset();
        currentZ = bone.localEulerAngles.z;
        Baseball.SetActive(true);
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
