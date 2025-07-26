using UnityEngine;
using System.Collections;
public class Stage4 : StageBase
{

    public Stage4_Player Stage4_Player;
    private bool stage2Next;
    private bool isGround;

    protected override void OnEnable()
    {
        playTime = 6f;
        stage2Next = false;
        base.OnEnable();
        timeclear = true;
        finishTime = 0;
    }

    private void Update()
    {
        if (Stage4_Player.isCrash && !stage2Next)
        {
            Debug.Log("실패 처리");
            StartCoroutine(FailEnding());
            stage2Next = true;

        }
    }

}
