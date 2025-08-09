using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreStage : MonoBehaviour
{
    public StageManager stageManager;
    private int level;
    private float prevScore;
    private float playTime = 3.5f;
    public GameObject[] lifeIcon;

    public TMP_Text textCount;

    public TMP_Text bonus;

    public GameObject timeBonus;
    public GameObject GameOver;
    private int prevLife; // 이전 Life 저장용 변수

    void OnEnable()
    {
        playTime = 4;
        level = stageManager.StageLevel;
        playTime = playTime - (0.2f * (float)level);

        Debug.Log(playTime);

        // timbonus 표시 및 오브젝트 활성/비활성
        if (stageManager.timbonus <= 0)
        {
            timeBonus.SetActive(false);
        }
        else
        {
            timeBonus.SetActive(true);
            bonus.text = $"{stageManager.timbonus}";
        }

        // Life 변경 감지
        if (stageManager.Life != prevLife)
        {
            SoundManager.Instance.Play("Pen");

            // 배열 범위 안전 체크
            if (stageManager.Life >= 0 && stageManager.Life < lifeIcon.Length)
            {
                lifeIcon[stageManager.Life].SetActive(true);
            }


        }

        prevLife = stageManager.Life; // 현재 값 저장

        if (prevScore != stageManager.scoreNum)
        {
            StartCoroutine(numCount(stageManager.scoreNum));
        }

        StartCoroutine(Playtime());


    }


    private IEnumerator Playtime()
    {


        yield return new WaitForSeconds(playTime);

        stageManager.currentState = StageState.Read;
        stageManager.isStagePlaying = false;

        if (stageManager.Life == 0)
        {
            GameOver.SetActive(true);
        }

    }

    IEnumerator numCount(float fTargetNum)
    {
        float elapsed = 0f;
        float start = prevScore;
        SoundManager.Instance.Play("Score_UP");

        while (elapsed < ((playTime - 1.5f) * 0.7f) )
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
