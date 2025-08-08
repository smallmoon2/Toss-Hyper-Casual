using UnityEngine;

public class Stage5 : StageBase
{
    public Stage5_Clear stage5_Clear;
    public Stage5_LineDraw lineDrawer;
    private bool stage2Next;
    protected override void OnEnable()
    {
        maxPlayTime = 5f;  // �ִ� �ð�
        minPlayTime = 3f;  // �ּ� �ð�
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
            lineDrawer.isFinished = true;
            Debug.Log("���� ó��");
            MissionClear();
            stage2Next = true;
        }
    }
}
