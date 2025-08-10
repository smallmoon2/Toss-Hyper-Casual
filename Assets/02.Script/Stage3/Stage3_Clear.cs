using System.Collections;
using UnityEngine;

public class Stage3_Clear : StageClearBase
{

    public Transform MomObject;
    public Transform BabyObject;
    public Transform StartPos;
    public Transform EndPos;
    public float moveDuration = 1.5f;

    public bool isOtherVersion = false; // ���� ���� ���� ����

    private Vector2 currentOffset = new Vector2(-1.55f, -0.6f);

    protected override void OnEnable()
    {

        base.OnEnable();
    }

    protected override IEnumerator Clear()
    {
        Debug.Log("??");
        yield return StartCoroutine(FamilyMove());
        ActivateMaskChildren(MomObject,2);
        ActivateMaskChildren(BabyObject,2);

        SoundManager.Instance.Play("Clear_1_2");
        // ���� Clear ���� ���� �߰� ����
    }

    protected override IEnumerator Fail()
    {
        Debug.Log("??");
        yield return StartCoroutine(FamilyMove());
        ActivateMaskChildren(MomObject, 1);
        ActivateMaskChildren(BabyObject, 1);

        SoundManager.Instance.Play("Fail_1");

        // ���� Fail ���� ���� �߰� ����
    }

    private IEnumerator FamilyMove()
    {
        SoundManager.Instance.PlayLoop("Walk");

        Animator momAnim = MomObject?.GetComponent<Animator>();
        Animator babyAnim = BabyObject?.GetComponent<Animator>();
        if (momAnim != null) momAnim.SetBool("IsRun", true);
        if (babyAnim != null) babyAnim.SetBool("IsRun", true);

        // ���� ������ ���� ��ġ(Reset���� �ٲ� ����)
        Vector3 momStart = MomObject != null ? MomObject.position : StartPos.position;
        Vector3 babyStart = BabyObject != null ? BabyObject.position : StartPos.position + (Vector3)currentOffset;

        // ������ ��ǥ ������ ���� ������ (-1.55, -0.6) ����
        Vector3 momEnd = EndPos.position;
        Vector3 babyEnd = EndPos.position + new Vector3(-1.55f, -0.6f, 0f);

        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);

            if (MomObject != null) MomObject.position = Vector3.Lerp(momStart, momEnd, t);
            if (BabyObject != null) BabyObject.position = Vector3.Lerp(babyStart, babyEnd, t);

            yield return null;
        }

        if (MomObject != null) MomObject.position = momEnd;
        if (BabyObject != null) BabyObject.position = babyEnd;

        SoundManager.Instance.Stop();
        if (momAnim != null) momAnim.SetBool("IsRun", false);
        if (babyAnim != null) babyAnim.SetBool("IsRun", false);
    }


    private void ActivateMaskChildren(Transform parent, int num)
    {
        Transform mask = parent?.Find("Mask");

        if (mask == null)
        {
            Debug.LogWarning($"{parent?.name}�� Mask ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        if (num < 0 || num >= mask.childCount)
        {
            Debug.LogWarning($"{parent?.name}�� Mask�� {num}�� �ڽ��� �������� �ʽ��ϴ�.");
            return;
        }

        for (int i = 0; i < mask.childCount; i++)
        {
            mask.GetChild(i).gameObject.SetActive(i == num);
        }

        Debug.Log($"{parent.name}�� Mask �ڽ� �� {num}���� Ȱ��ȭ �Ϸ�");
    }


    private void ApplyReset(float offsetX, float offsetY, int maskIndex)
    {
        // �� ���õ� ������ ���� (FamilyMove���� ������ ��꿡 ���)
        currentOffset = new Vector2(offsetX, offsetY);

        if (MomObject != null) MomObject.position = StartPos.position;
        if (BabyObject != null) BabyObject.position = StartPos.position + new Vector3(offsetX, offsetY, 0f);

        ActivateMaskChildren(MomObject, maskIndex);
        ActivateMaskChildren(BabyObject, maskIndex);
    }
    public void GameReset()
    {
        if (isOtherVersion)
        {
            ApplyReset(offsetX: -5.55f, offsetY: -0.6f, maskIndex: 3);
            SetBabyRotationY(0f);      // �� ��° ����: Y = 0
        }
        else
        {
            ApplyReset(offsetX: -1.55f, offsetY: -0.6f, maskIndex: 0);
            SetBabyRotationY(180f);    // ù ��° ����: Y = 180
        }

        isOtherVersion = !isOtherVersion;
    }

    private void SetBabyRotationY(float yValue)
    {
        if (BabyObject != null)
        {
            Vector3 rot = BabyObject.eulerAngles;
            rot.y = yValue;
            BabyObject.eulerAngles = rot;
        }
    }

}
