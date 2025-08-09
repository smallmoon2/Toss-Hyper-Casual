using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;              // 효과음 이름 (key)
    public AudioClip clip;          // 실제 사운드 클립
    [Range(0f, 1f)]
    public float volume = 1f;       // 볼륨 설정
}