using UnityEngine;
using System.Collections;
public class Stage4 : StageBase
{
    public Stage4_Clear Stage4_Clear;
    public Stage4_Player Stage4_Player;
    public bool stage4Next;
    private bool isGround;
    private Animator anim;
    protected override void OnEnable()
    {
        maxPlayTime = 5f;  // �ִ� �ð�
        minPlayTime = 5f;  // �ּ� �ð�
        playTime = 6f;
        stage4Next = false;
        base.OnEnable();

        Stage4_Clear.GameReset();
        timeclear = true;
        finishTime = 0;

        anim = player.GetComponent<Animator>();

    }

    private void Update()
    {
        if (Stage4_Player.isCrash && !stage4Next)
        {
            StartCoroutine(FailEnding());
            stage4Next = true;

        }
    }

    protected override IEnumerator FailEnding()
    {

        if (isFinshed) yield break;  //  �ߺ� ����
        isFinshed = true;

        prograssbar.fillAmount = 1f;
        yield return new WaitForSeconds(finishTime);
        failAction.SetActive(true);
        stageManager.Life--;

        yield return new WaitForSeconds(endingTime);

        Debug.Log("�������̵�");
        anim.SetBool("IsFowardDown", false);
        stageManager.isStagenext = true;
    }


}
