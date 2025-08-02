using UnityEngine;
using static StarCatch;

public class Stage10 : StageBase
{
    public StarCatch starCatch;
    private bool stage10Next;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void OnEnable()
    {
        finishTime = 0.5f;
        playTime = 5f;
        base.OnEnable();
        stage10Next = false;

    }

    void Update()
    {

        if (starCatch.isCatch == isStarCatch.Clear && !stage10Next)
        {
            Debug.Log("己傍 贸府");
            StartCoroutine(ClearEnding());
            stage10Next = true;
        }

        if (starCatch.isCatch == isStarCatch.Fail && !stage10Next)
        {
            Debug.Log("角菩 贸府");
            StartCoroutine(FailEnding());
            stage10Next = true;
        }
    }
}
