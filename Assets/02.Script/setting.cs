using UnityEngine;

public class Setting : MonoBehaviour
{
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
    }

    public void OnClick_CloseSettings() // �ݱ�/�ڷ� ��ư
    {
        PauseManager.Instance?.CloseSettings();
    }
}
