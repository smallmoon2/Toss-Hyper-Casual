using System.Collections;
using UnityEngine;

public class Stage4_Clear : StageClearBase
{
    public Transform blueObject;
    private Animator animator;

    protected override void OnEnable()
    {
        
        animator = blueObject.GetComponent<Animator>();
        base.OnEnable();

    }

    protected override IEnumerator Clear()
    {
        animator.SetTrigger("IsWin");
        ActivateMaskChildren(blueObject, 2);
        yield return null;
    }

    protected override IEnumerator Fail()
    {
        animator.SetBool("IsFowardDown", true);

        ActivateMaskChildren(blueObject, 3);
        Debug.Log("실패");

        

        yield return null;
    }





    private void ActivateMaskChildren(Transform parent, int num)
    {
        Transform mask = FindDeepChild(parent, "Mask");  // 변경된 부분

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

    private Transform FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;

            Transform result = FindDeepChild(child, name);
            if (result != null)
                return result;
        }
        return null;
    }
    public void GameReset()
    {
        ActivateMaskChildren(blueObject, 0);
    }
}
