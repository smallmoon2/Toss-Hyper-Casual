using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreStage : MonoBehaviour
{
    public StageManager stageManager;
    private int level;
    private float prevScore;
    private float playTime = 3.2f;
    public GameObject[] lifeIcon;

    public GameObject[] Dancer;

    public TMP_Text textCount;

    public RevealEffect_Right[] Reveal;

    public RevealEffect[] Reveal2;

    public TMP_Text bonus;

    public GameObject timeBonus;
    public GameObject GameOver;

    public Button reStartButton;

    public bool isReSet;

    private int prevLife; // ���� Life ����� ����

    void OnEnable()
    {

        reStartButton.onClick.AddListener(ReStart);

        playTime = 3.2f;
        level = stageManager.StageLevel;
        playTime = playTime - (0.2f * (float)level);

        Debug.Log(playTime);

        // timbonus ǥ�� �� ������Ʈ Ȱ��/��Ȱ��
        if (stageManager.timbonus <= 0)
        {
            timeBonus.SetActive(false);
        }
        else
        {
            timeBonus.SetActive(true);
            bonus.text = $"{stageManager.timbonus}";
        }

        // Life ���� ����
        if (stageManager.Life != prevLife)
        {
            

            // �迭 ���� ���� üũ
            if (stageManager.Life >= 0 && stageManager.Life < lifeIcon.Length)
            {
                SoundManager.Instance.Play("Pen");
                lifeIcon[stageManager.Life].SetActive(true);
                Dancer[stageManager.Life].SetActive(false);
            }


        }

        prevLife = stageManager.Life; // ���� �� ����

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
        float duration = 1.5f; // ���� ������ �ð� ����

        SoundManager.Instance.Play("Score_UP");

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float percent = Mathf.Clamp01(elapsed / duration);
            float current = Mathf.Lerp(start, fTargetNum, percent);
            textCount.text = current.ToString("F0");
            yield return null;
        }

        textCount.text = fTargetNum.ToString("F0");
        prevScore = fTargetNum;
    }



    private void ReStart()
    {
        isReSet = false;
        stageManager.Life = 3;
        stageManager.StageLevel = 1;
        stageManager.scoreNum = 0;
        stageManager.timbonus = 0;
        stageManager.curStage = 0;
        stageManager.currentState = StageState.Read;
        GameOver.SetActive(false);
        textCount.text = "0";
        prevScore = 0;
        prevLife = 3;
        for (int i = 0; i < 3; i++)
        {
            lifeIcon[i].SetActive(false);
            Dancer[i].SetActive(true);
        }


        for (int i = 0; i < 3; i++)
        {
            Reveal[i].isStart = false;
        }

        for (int i = 0; i < 3; i++)
        {
            Reveal2[i].isStart = false;
        }
    }

}
