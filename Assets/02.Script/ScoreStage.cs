using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreStage : MonoBehaviour
{
    public StageManager stageManager;
    private int level;
    private float playTime = 5;
    public GameObject[] lifeIcon;


    void OnEnable()
    {
        level = stageManager.StageLevel;
        playTime = playTime - (0.7f * (float)level);

        if (stageManager.Life < 3)
        {
            lifeIcon[stageManager.Life].SetActive(true);
        }

        StartCoroutine(Playtime());
    }


    private IEnumerator Playtime()
    {

        Debug.Log("Ãâ·Â¾ÈµÊ");
        yield return new WaitForSeconds(playTime);

        stageManager.currentState = StageState.Read;
        stageManager.isStagePlaying = false;


    }
}
