using UnityEngine;

public class SoundAnim : MonoBehaviour
{
    // 애니메이션에서 호출할 함수
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