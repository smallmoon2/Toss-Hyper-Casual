using UnityEngine;

public class Stage5 : StageBase
{
    public Stage5_Clear stage5_Clear;
    public Stage5_LineDraw lineDrawer;
    private bool stage2Next;
    protected override void OnEnable()
    {
        finishTime = 0.5f;
        playTime = 10f;
        endingTime = 2f;
        base.OnEnable();
        stage2Next = false;
    }

    private void Update()
    {


        if (lineDrawer.lineCount >= 12 && !stage2Next)
        {
            Debug.Log("성공 처리");
            StartCoroutine(ClearEnding());
            stage2Next = true;
        }
    }
}
