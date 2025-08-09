using UnityEngine;

public class SoundAnim : MonoBehaviour
{
    // �ִϸ��̼ǿ��� ȣ���� �Լ�
    public void PlaySound(string soundName)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Play(soundName);
        }
        else
        {
            Debug.LogWarning("SoundManager not found!");
        }
    }
}