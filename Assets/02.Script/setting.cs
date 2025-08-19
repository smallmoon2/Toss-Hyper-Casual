using UnityEngine;

public class Setting : MonoBehaviour
{
    public ScoreStage scoreStage;
    public GameObject SoundOF;
    


    // �� �� ���� ������ ȿ���� ��� (�ѱ�/����)
    public void OnClick_ToggleSfx()
    {
        if (SoundManager.Instance != null)
        {
            Debug.Log("����");
            SoundManager.Instance.ToggleSfx();
            SoundOF.SetActive(!SoundOF.activeSelf);
        }

            
    }
    public void OnClick_OpenSettings()  // ���� ��ư
    {
        PauseManager.Instance?.OpenSettings();
        
        Debug.Log("���� Ȱ��ȭ");

    }

    public void OnClick_CloseSettings() // �ݱ�/�ڷ� ��ư
    {
        PauseManager.Instance?.CloseSettings();
    }

    public void Gamereset() // ����
    {
        if (PauseManager.Instance.isResetting)
        {
            scoreStage.ReStart();
            PauseManager.Instance.isResetting = false;
        }
        
    }
}
