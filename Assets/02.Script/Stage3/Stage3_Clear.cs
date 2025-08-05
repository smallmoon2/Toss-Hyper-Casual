using System.Collections;
using UnityEngine;

public class Stage3_Clear : StageClearBase
{

    public Transform MomObject;
    public Transform BabyObject;
    public Transform StartPos;
    public Transform EndPos;
    public float moveDuration = 1.5f;


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
        // ���� Clear ���� ���� �߰� ����
    }

    protected override IEnumerator Fail()
    {
        Debug.Log("??");
        yield return StartCoroutine(FamilyMove());
        ActivateMaskChildren(MomObject, 1);
        ActivateMaskChildren(BabyObject, 1);

        // ���� Fail ���� ���� �߰� ����
    }

    private IEnumerator FamilyMove()
    {
        if (MomObject != null) MomObject.position = StartPos.position;
        if (BabyObject != null)
            BabyObject.position = StartPos.position + new Vector3(-1.55f, -0.6f, 0f);

        // �ִϸ��̼� Ȱ��ȭ
        Animator momAnim = MomObject?.GetComponent<Animator>();
        Animator babyAnim = BabyObject?.GetComponent<Animator>();

        if (momAnim != null) momAnim.SetBool("IsRun", true);
        if (babyAnim != null) babyAnim.SetBool("IsRun", true);

        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);

            if (MomObject != null)
                MomObject.position = Vector3.Lerp(StartPos.position, EndPos.position, t);

            if (BabyObject != null)
            {
                Vector3 babyStart = StartPos.position + new Vector3(-1.55f, -0.6f, 0f);
                Vector3 babyEnd = EndPos.position + new Vector3(-1.55f, -0.6f, 0f);
                BabyObject.position = Vector3.Lerp(babyStart, babyEnd, t);
            }

            yield return null;
        }

        // ���� ��ġ ����
        if (MomObject != null) MomObject.position = EndPos.position;
        if (BabyObject != null)
            BabyObject.position = EndPos.position + new Vector3(-1.55f, -0.6f, 0f);

        //// �ִϸ��̼� ����
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

    public void GameReset()
    {
        if (MomObject != null) MomObject.position = StartPos.position;
        if (BabyObject != null)
            BabyObject.position = StartPos.position + new Vector3(-1.55f, -0.6f, 0f);
        ActivateMaskChildren(MomObject, 0);
        ActivateMaskChildren(BabyObject, 0);
    }
}
