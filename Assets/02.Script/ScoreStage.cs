using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreStage : MonoBehaviour
{
    public Fade fade;

    public Image prograssbar;
    public Stage1 stage1;
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

    private int prevLife; // 이전 Life 저장용 변수

    void OnEnable()
    {

        reStartButton.onClick.AddListener(ReStart);

        playTime = 3.2f;
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
            

            // 배열 범위 안전 체크
            if (stageManager.Life >= 0 && stageManager.Life < lifeIcon.Length)
            {
                SoundManager.Instance.Play("Pen");
                lifeIcon[stageManager.Life].SetActive(true);
                Dancer[stageManager.Life].SetActive(false);
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
        float duration = 1.5f; // 점수 오르는 시간 고정

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



    public void ReStart()
    {
        // stageManager는 항상 활성이라고 가정
        stageManager.StartCoroutine(ReStartRoutine());
    }

    private IEnumerator ReStartRoutine()
    {
        // 현재 씬에서 활성화된 Animator 전부 찾기
        Animator[] animators = Object.FindObjectsByType<Animator>(FindObjectsSortMode.None);
        fade.PlayScaleEffect();
        PauseManager.Instance?.CloseSettings();
        foreach (Animator anim in animators)
        {
            if (anim != null && anim.isActiveAndEnabled) // 활성 오브젝트 + 켜져있는 Animator만
            {
                anim.SetBool("IsFowardDown", false);
                anim.SetBool("IsSliding", false);
                anim.SetBool("IsWalkB", false);
                anim.SetBool("IsJump", false);
                anim.SetBool("IsRun", false);
                anim.SetTrigger("AllReset");
                anim.SetTrigger("IsSnackR");
            }
        }

        // Time.timeScale = 0 이어도 1초 기다리게
        yield return new WaitForSecondsRealtime(1f);

        PerformReset();
    }
    private void PerformReset()
    {


        prograssbar.fillAmount = 1f;
        stageManager.Stages[stageManager.curStage].SetActive(false);
        stageManager.isStagePlaying = false;
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
        stage1.first = false;
        SoundManager.Instance?.StopAllSfxKeepBgm();

        for (int i = 0; i < 3; i++) { lifeIcon[i].SetActive(false); Dancer[i].SetActive(true); }
        for (int i = 0; i < 3; i++) { Reveal[i].isStart = false; }
        for (int i = 0; i < 3; i++) { Reveal2[i].isStart = false; }
    }


}
