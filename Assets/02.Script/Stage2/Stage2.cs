using UnityEngine;

public class Stage2 : StageBase
{
    public LineDrawBlocker2D lineDrawer;
    private bool stage2Next;
    protected override void OnEnable()
    {
        finishTime = 0.5f;
        playTime = 20f;
        endingTime = 2f;
        base.OnEnable();
        stage2Next = false;

    }

    private void Update()
    {
        if (lineDrawer.iscrash && !stage2Next)
        {
            Debug.Log("角菩 贸府");
            StartCoroutine(FailEnding());
            stage2Next = true;

        }

        if (lineDrawer.isGoalReached && !stage2Next )
        {
            Debug.Log("己傍 贸府");
            StartCoroutine(ClearEnding());
            stage2Next = true;
        }
    }

}
