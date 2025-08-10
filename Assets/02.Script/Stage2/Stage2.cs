using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Stage2 : StageBase
{
    public LineDrawBlocker2D lineDrawer;
    private bool stage2Next;
    public GameObject failanim;
    private Animator anim;
    protected override void OnEnable()
    {
        maxPlayTime = 4f;  // 최대 시간
        minPlayTime = 2.5f;  // 최소 시간
        finishTime = 0.5f;
        playTime = 20f;
        endingTime = 2.5f;
        base.OnEnable();
        stage2Next = false;
        anim = failanim.GetComponent<Animator>();
    }

    private void Update()
    {
        if (lineDrawer.iscrash && !stage2Next)
        {
   
            StartCoroutine(FailEnding());
            stage2Next = true;

        }

        if (lineDrawer.isGoalReached && !stage2Next )
        {

            MissionClear();
            stage2Next = true;
        }
    }

    protected override IEnumerator FailEnding()
    {

        prograssbar.fillAmount = 1f;
        yield return new WaitForSeconds(finishTime);
        failAction.SetActive(true);
        stageManager.Life--;

        yield return new WaitForSeconds(endingTime);

        Debug.Log("오버라이딩");
        anim.SetBool("IsFowardDown", false);
        stageManager.isStagenext = true;
    }
}
