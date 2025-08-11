using System.Collections;
using UnityEngine;
using static StarCatch;


public class Stage10 : StageBase
{
    public StarCatch starCatch;
    private bool stage10Next;
    public Transform babyObject;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void OnEnable()
    {

        maxPlayTime = 5f;  // �ִ� �ð�
        minPlayTime = 3f;  // �ּ� �ð�
        anim = player.GetComponent<Animator>();

        anim.SetBool("IsSnack", true);

        finishTime = 0.5f;
        playTime = 5f;
        base.OnEnable();
        stage10Next = false;

    }

    void Update()
    {

        if (starCatch.isCatch == isStarCatch.Clear && !stage10Next)
        {
            Debug.Log("���� ó��");
            anim.SetTrigger("IsSnackC");
            ActivateMaskChildren(babyObject, 2);
            
            SoundManager.Instance.Play("Snack Popping");
            MissionClear();
            stage10Next = true;
        }

        if (starCatch.isCatch == isStarCatch.Fail && !stage10Next)
        {
          
            ActivateMaskChildren(babyObject, 3);
            Debug.Log("���� ó��");
            anim.SetTrigger("IsSnackF");
            SoundManager.Instance.Play("Snack Popping");

            StartCoroutine(FailEnding());
            stage10Next = true;
        }
    }
    protected override IEnumerator FailEnding()
    {

        prograssbar.fillAmount = 1f;
        yield return new WaitForSeconds(finishTime);

        failAction.SetActive(true);
        stageManager.Life--;
        anim.SetBool("IsSnack", false);
        SoundManager.Instance.Play("Sad");
        yield return new WaitForSeconds(endingTime);
        stageManager.timbonus = 0;
        Debug.Log("�������̵�");
        anim.SetTrigger("IsSnackR");
        stageManager.isStagenext = true;
    }
    protected override IEnumerator ClearEnding()
    {
        isClear = true;
        stageManager.scoreNum += 100 + stageManager.timbonus;
        prograssbar.fillAmount = 1f;
        yield return new WaitForSeconds(finishTime);

        SoundManager.Instance.Play("Clear_1_1");
        clearAction.SetActive(true);
        Setreset();
        anim.SetBool("IsSnack", false);
        yield return new WaitForSeconds(endingTime);
        anim.SetTrigger("IsSnackR");
        stageManager.isStagenext = true;
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

}
