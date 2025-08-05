using UnityEngine;
using System.Collections;
public class Stage4 : StageBase
{
    public Stage4_Clear Stage4_Clear;
    public Stage4_Player Stage4_Player;
    public bool stage4Next;
    private bool isGround;

    protected override void OnEnable()
    {
        playTime = 6f;
        stage4Next = false;
        base.OnEnable();

        Stage4_Clear.GameReset();
        timeclear = true;
        finishTime = 0;

    }

    private void Update()
    {
        if (Stage4_Player.isCrash && !stage4Next)
        {
            Debug.Log("실패 처리");
            StartCoroutine(FailEnding());
            stage4Next = true;

        }
    }

}
