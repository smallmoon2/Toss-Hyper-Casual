using UnityEngine;
using System.Collections.Generic;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public List<Sound> sounds;
    private string currentLoopSound = null; // 현재 루프 사운드 이름 저장

    private Dictionary<string, Sound> soundDict;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        soundDict = new Dictionary<string, Sound>();

        foreach (Sound s in sounds)
        {
            if (!soundDict.ContainsKey(s.name))
            {
                soundDict.Add(s.name, s);
            }
        }
    }

    public void Play(string soundName)
    {
        if (soundDict.TryGetValue(soundName, out Sound sound))
        {
            audioSource.PlayOneShot(sound.clip, sound.volume);
        }
        else
        {
            Debug.LogWarning($"Sound '{soundName}' not found!");
        }
    }

    public void PlayLoop(string soundName)
    {
        // 이미 같은 사운드가 루프로 재생 중이면 중복 실행 안 함
        if (audioSource.isPlaying && soundName == currentLoopSound)
            return;

        if (soundDict.TryGetValue(soundName, out Sound sound))
        {
            currentLoopSound = soundName;
            audioSource.clip = sound.clip;
            audioSource.volume = sound.volume;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"Loop sound '{soundName}' not found!");
        }
    }


    public void Stop()
    {
        audioSource.Stop();
        currentLoopSound = null; // 정지 시 상태 초기화
    }
}
