using UnityEngine;

public class BestScore : MonoBehaviour
{
    const string KEY = "BEST_SCORE";
    public int BestScoreNum { get; private set; }

    void Awake()
    {
        // 시작 시 저장된 값 읽기 (없으면 0)
        BestScoreNum = PlayerPrefs.GetInt(KEY, 0);
    }

    /// <summary>
    /// 이번 플레이 기록을 넘기면 자동 저장
    /// </summary>
    public bool TrySetNewScore(int currentScore)
    {
        if (currentScore <= BestScoreNum) return false;

        BestScoreNum = currentScore;
        PlayerPrefs.SetInt(KEY, BestScoreNum);
        PlayerPrefs.Save();   // WebGL에선 즉시 저장을 권장
        return true;
    }

    public void ResetBestScore()  // 디버그용 초기화
    {
        BestScoreNum = 0;
        PlayerPrefs.DeleteKey(KEY);
        PlayerPrefs.Save();
    }
}
