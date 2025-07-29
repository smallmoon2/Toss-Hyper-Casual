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

    public bool isStagePlaying = false;
    public bool isStagenext = false;
    void Start()
    {
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
            // ScoreStage ��Ȱ��ȭ �� ���� stage Ȱ��ȭ
            Debug.Log("�����ܰ� Ȱ��ȭ");
            scoreStage.SetActive(false);
            Stages[curStage].SetActive(false);
            Stages[curStage].SetActive(true);
            isStagePlaying = true;
        }
        else
        {
            if (isStagenext == true)
            {
                Stages[curStage].SetActive(false);
                scoreStage.SetActive(true);
                

                Debug.Log("�����ܰ� �̵�");
                curStage = (curStage + 1) % Stages.Length;

                isStagenext = false;
            }
        }
        
    }

    protected virtual void OnEnd()
    {
        Debug.Log("StageManager_Gameover");
    }


}