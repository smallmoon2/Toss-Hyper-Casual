using UnityEngine;

public enum StageState { Read, Play, End }

public class StageManager : MonoBehaviour
{
    public StageState currentState;

    public GameObject[] Stages;
    public GameObject scoreStage;
    public int curStage;
    public int StageLevel;
    public int Life = 3;
    public int scoreNum;

    public int timbonus;
    public bool isStagePlaying = false;
    public bool isStagenext = false;
    void Start()
    {
        scoreNum = 0;
        timbonus = 0;
        StageLevel = 1;
        currentState = StageState.Read;
    }

    void Update()
    {
        ChangeState(currentState);

    }

    protected void ChangeState(StageState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case StageState.Read:
                OnRead();
                break;
            case StageState.Play:
                OnPlay();
                break;
            case StageState.End:
                OnEnd();
                break;
        }
    }

    protected virtual void OnRead()
    {
        if (Life == 0)
        {
            Debug.Log(Life);
            currentState = StageState.End;
        }
        else
        {
            
            currentState = StageState.Play;    
        }
    }

    protected virtual void OnPlay()
    {
        if (isStagePlaying == false)
        {
            scoreStage.SetActive(false);
            Stages[curStage].SetActive(false);
            Stages[curStage].SetActive(true);
            isStagePlaying = true;
        }
        else
        {
            if (isStagenext == true)
            {
                scoreStage.SetActive(true);
                Stages[curStage].SetActive(false);

                // 마지막 스테이지였는지 확인 후 StageLevel 증가
                if (curStage == Stages.Length - 1 && StageLevel < 4)
                {
                    StageLevel++;
                }

                // 다음 스테이지로 이동
                curStage = (curStage + 1) % Stages.Length;

                isStagenext = false;

            }
        }
    }


    protected virtual void OnEnd()
    {

    }


}