using UnityEngine;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameStage
    {
        Intro,
        Stage1,
        Stage2,
        Ending
    }
    public GameStage CurrentStage { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextStage()
    {
        switch (CurrentStage)
        {
            case GameStage.Intro:
                SetStage(GameStage.Stage1);
                break;
            case GameStage.Stage1:
                SetStage(GameStage.Stage2);
                break;
            case GameStage.Stage2:
                SetStage(GameStage.Ending);
                break;
        }
    }

    public void SetStage(GameStage stage)
    {
        CurrentStage = stage;
    }
}