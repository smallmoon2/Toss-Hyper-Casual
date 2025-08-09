using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;              // ȿ���� �̸� (key)
    public AudioClip clip;          // ���� ���� Ŭ��
    [Range(0f, 1f)]
    public float volume = 1f;       // ���� ����
}