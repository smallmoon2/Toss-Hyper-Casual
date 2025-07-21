using UnityEngine;

public enum StageState { Read, Play, End }

public class StageManager : MonoBehaviour
{
    protected StageState currentState;

    public int StageLevel;


    void Start()
    {
        StageLevel = 1;
        currentState = StageState.Read;
    }

    void Update()
    {
        ChangeState(StageState.End);

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

    }

    protected virtual void OnPlay()
    {

    }

    protected virtual void OnEnd()
    {

    }


}