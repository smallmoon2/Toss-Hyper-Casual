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
        // 이후 Clear 전용 로직 추가 가능
    }

    protected override IEnumerator Fail()
    {
        Debug.Log("??");
        yield return StartCoroutine(FamilyMove());
        ActivateMaskChildren(MomObject, 1);
        ActivateMaskChildren(BabyObject, 1);

        // 이후 Fail 전용 로직 추가 가능
    }

    private IEnumerator FamilyMove()
    {
        if (MomObject != null) MomObject.position = StartPos.position;
        if (BabyObject != null)
            BabyObject.position = StartPos.position + new Vector3(-1.55f, -0.6f, 0f);

        // 애니메이션 활성화
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

        // 최종 위치 고정
        if (MomObject != null) MomObject.position = EndPos.position;
        if (BabyObject != null)
            BabyObject.position = EndPos.position + new Vector3(-1.55f, -0.6f, 0f);

        //// 애니메이션 종료
        if (momAnim != null) momAnim.SetBool("IsRun", false);
        if (babyAnim != null) babyAnim.SetBool("IsRun", false);
    }

    private void ActivateMaskChildren(Transform parent, int num)
    {
        Transform mask = parent?.Find("Mask");

        if (mask == null)
        {
            Debug.LogWarning($"{parent?.name}의 Mask 오브젝트를 찾을 수 없습니다.");
            return;
        }

        if (num < 0 || num >= mask.childCount)
        {
            Debug.LogWarning($"{parent?.name}의 Mask에 {num}번 자식이 존재하지 않습니다.");
            return;
        }

        for (int i = 0; i < mask.childCount; i++)
        {
            mask.GetChild(i).gameObject.SetActive(i == num);
        }

        Debug.Log($"{parent.name}의 Mask 자식 중 {num}번만 활성화 완료");
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
