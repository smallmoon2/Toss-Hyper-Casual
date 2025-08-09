using System.Collections;
using UnityEngine;

public class Stage6_Clear : StageClearBase
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
        animator.SetBool("IsRun", false);
        SoundManager.Instance.Stop();
        ActivateMaskChildren(blueObject, 1);
        SoundManager.Instance.Play("Fail_1");



        yield return null;
    }





    private void ActivateMaskChildren(Transform parent, int num)
    {
        Transform mask = FindDeepChild(parent, "Mask");  // ����� �κ�

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
