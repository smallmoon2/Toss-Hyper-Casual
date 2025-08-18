using UnityEngine;

public class BestScore : MonoBehaviour
{
    const string KEY = "BEST_SCORE";
    public int BestScoreNum { get; private set; }

    void Awake()
    {
        // ���� �� ����� �� �б� (������ 0)
        BestScoreNum = PlayerPrefs.GetInt(KEY, 0);
    }

    /// <summary>
    /// �̹� �÷��� ����� �ѱ�� �ڵ� ����
    /// </summary>
    public bool TrySetNewScore(int currentScore)
    {
        if (currentScore <= BestScoreNum) return false;

        BestScoreNum = currentScore;
        PlayerPrefs.SetInt(KEY, BestScoreNum);
        PlayerPrefs.Save();   // WebGL���� ��� ������ ����
        return true;
    }

    public void ResetBestScore()  // ����׿� �ʱ�ȭ
    {
        BestScoreNum = 0;
        PlayerPrefs.DeleteKey(KEY);
        PlayerPrefs.Save();
    }
}
