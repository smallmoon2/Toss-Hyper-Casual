using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreStage : MonoBehaviour
{
    public StageManager stageManager;
    private int level;
    private float prevScore;
    private float playTime = 5;
    public GameObject[] lifeIcon;

    public TMP_Text textCount;




    void OnEnable()
    {
        playTime = 4;
        level = stageManager.StageLevel;
        playTime = playTime - (0.7f * (float)level);

        Debug.Log(playTime);

        if (stageManager.Life < 3)
        {
            lifeIcon[stageManager.Life].SetActive(true);
        }
        StartCoroutine(numCount(stageManager.scoreNum));
        StartCoroutine(Playtime());
    }


    private IEnumerator Playtime()
    {


        yield return new WaitForSeconds(playTime);

        stageManager.currentState = StageState.Read;
        stageManager.isStagePlaying = false;


    }

    IEnumerator numCount(float fTargetNum)
    {
        float elapsed = 0f;
        float start = prevScore;

        while (elapsed < (playTime * 0.7f) )
        {
            elapsed += Time.deltaTime;
            float percent = Mathf.Clamp01(elapsed / playTime);
            float current = Mathf.Lerp(start, fTargetNum, percent);
            textCount.text = current.ToString("F0");
            yield return null;
        }

        textCount.text = fTargetNum.ToString("F0");

        prevScore = fTargetNum;
    }
}
