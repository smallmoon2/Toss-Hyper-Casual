using System.Collections;
using UnityEngine;

public class Stage9_Clear : StageClearBase
{
    public Stage9_BaseBall baseBall;
    public Transform blueObject;
    public Transform babyObject;

    public GameObject Sad;
    public GameObject Head_Hit;


    protected override void OnEnable()
    {


        base.OnEnable();
    }

    protected override IEnumerator Clear()
    {
        SoundManager.Instance.Play("Clear_2_1");
        ActivateMaskChildren(blueObject, 2);
        ActivateMaskChildren(babyObject, 2);
        yield return null;
    }

    protected override IEnumerator Fail()
    {
        if (baseBall.babyTurn)
        {
            Sad.SetActive(true);
        }
        else
        {
            Head_Hit.SetActive(true);
        }
        SoundManager.Instance.Play("Sad");
        Debug.Log("�����Ҷ�");
        ActivateMaskChildren(blueObject, 1);
        ActivateMaskChildren(babyObject, 3);


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
        ActivateMaskChildren(babyObject, 0);
        Sad.SetActive(false);
        Head_Hit.SetActive(false);
    }


}
