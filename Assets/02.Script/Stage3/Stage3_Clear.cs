using System.Collections;
using UnityEngine;

public class Stage3_Clear : StageClearBase
{

    public Transform MomObject;
    public Transform BabyObject;
    public Transform StartPos;
    public Transform EndPos;
    public float moveDuration = 1.5f;

    public bool isOtherVersion = false; // 현재 버전 상태 저장

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
        // 이후 Clear 전용 로직 추가 가능
    }

    protected override IEnumerator Fail()
    {
        Debug.Log("??");
        yield return StartCoroutine(FamilyMove());
        ActivateMaskChildren(MomObject, 1);
        ActivateMaskChildren(BabyObject, 1);

        SoundManager.Instance.Play("Fail_1");

        // 이후 Fail 전용 로직 추가 가능
    }

    private IEnumerator FamilyMove()
    {
        SoundManager.Instance.PlayLoop("Walk");

        Animator momAnim = MomObject?.GetComponent<Animator>();
        Animator babyAnim = BabyObject?.GetComponent<Animator>();
        if (momAnim != null) momAnim.SetBool("IsRun", true);
        if (babyAnim != null) babyAnim.SetBool("IsRun", true);

        // 시작 지점은 현재 위치(Reset으로 바뀐 상태)
        Vector3 momStart = MomObject != null ? MomObject.position : StartPos.position;
        Vector3 babyStart = BabyObject != null ? BabyObject.position : StartPos.position + (Vector3)currentOffset;

        // 마지막 목표 지점은 기존 오프셋 (-1.55, -0.6) 고정
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


    private void ApplyReset(float offsetX, float offsetY, int maskIndex)
    {
        // ★ 선택된 오프셋 저장 (FamilyMove에서 종착점 계산에 사용)
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
            SetBabyRotationY(0f);      // 두 번째 버전: Y = 0
        }
        else
        {
            ApplyReset(offsetX: -1.55f, offsetY: -0.6f, maskIndex: 0);
            SetBabyRotationY(180f);    // 첫 번째 버전: Y = 180
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
