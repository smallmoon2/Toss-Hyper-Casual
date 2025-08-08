using System.Collections;
using UnityEngine;
using static StarCatch;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Stage10 : StageBase
{
    public StarCatch starCatch;
    private bool stage10Next;


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
            
            StartCoroutine(ClearEnding());
            stage10Next = true;
        }

        if (starCatch.isCatch == isStarCatch.Fail && !stage10Next)
        {
            Debug.Log("���� ó��");
            anim.SetTrigger("IsSnackF");
            
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
        yield return new WaitForSeconds(endingTime);

        Debug.Log("�������̵�");
        anim.SetTrigger("IsSnackR");
        stageManager.isStagenext = true;
    }
    protected override IEnumerator ClearEnding()
    {
        isClear = true;
        stageManager.scoreNum = stageManager.scoreNum + 100;
        prograssbar.fillAmount = 1f;
        yield return new WaitForSeconds(finishTime);
        clearAction.SetActive(true);
        Setreset();
        anim.SetBool("IsSnack", false);
        yield return new WaitForSeconds(endingTime);
        anim.SetTrigger("IsSnackR");
        stageManager.isStagenext = true;
    }
}
