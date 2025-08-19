using UnityEngine;

public class Setting : MonoBehaviour
{
    public ScoreStage scoreStage;
    public GameObject SoundOF;
    


    // 한 번 누를 때마다 효과음 토글 (켜기/끄기)
    public void OnClick_ToggleSfx()
    {
        if (SoundManager.Instance != null)
        {
            Debug.Log("설정");
            SoundManager.Instance.ToggleSfx();
            SoundOF.SetActive(!SoundOF.activeSelf);
        }

            
    }
    public void OnClick_OpenSettings()  // 설정 버튼
    {
        PauseManager.Instance?.OpenSettings();
        
        Debug.Log("리셋 활성화");

    }

    public void OnClick_CloseSettings() // 닫기/뒤로 버튼
    {
        PauseManager.Instance?.CloseSettings();
    }

    public void Gamereset() // 리셋
    {
        if (PauseManager.Instance.isResetting)
        {
            scoreStage.ReStart();
            PauseManager.Instance.isResetting = false;
        }
        
    }
}
