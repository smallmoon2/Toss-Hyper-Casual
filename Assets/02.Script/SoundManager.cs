using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public static int HelpCount = 1;

    public List<Sound> sounds;
    private string currentLoopSound = null;

    private Dictionary<string, Sound> soundDict;

    private AudioSource bgmSource;

    private AudioSource sfxSource;   // 원샷 전용
    private AudioSource loopSource;  // 루프 전용 (기존 audioSource 대체)

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        bgmSource = gameObject.AddComponent<AudioSource>(); 
        bgmSource.loop = true;

        soundDict = new Dictionary<string, Sound>();
        foreach (Sound s in sounds)
        {
            if (!soundDict.ContainsKey(s.name))
                soundDict.Add(s.name, s);
        }
        loopSource = gameObject.AddComponent<AudioSource>();
        loopSource.loop = true;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;

    }

    private void Start()
    {
        SoundManager.Instance.PlayBGM("BlueMushBGM");
    }


    public void Play(string soundName)
    {
        if (soundDict.TryGetValue(soundName, out Sound sound))
            sfxSource.PlayOneShot(sound.clip, sound.volume); // loop 볼륨과 분리
    }

    public void PlayLoop(string soundName)
    {
        if (loopSource.isPlaying && soundName == currentLoopSound) return;
        if (soundDict.TryGetValue(soundName, out Sound sound))
        {
            currentLoopSound = soundName;
            loopSource.clip = sound.clip;
            loopSource.volume = sound.volume;
            loopSource.Play();
        }
    }

    public void Stop()
    {
        loopSource.Stop();
        currentLoopSound = null;
    }


    public void PlayBGM(string soundName)
    {
        if (soundDict.TryGetValue(soundName, out Sound sound))
        {
            bgmSource.clip = sound.clip;
            bgmSource.volume = sound.volume;
            bgmSource.Play();
        }
        else
            Debug.LogWarning($"BGM '{soundName}' not found!");
    }

    public void SetBGMVolume(float volume) // 0 ~ 1
    {
        bgmSource.volume = Mathf.Clamp01(volume);
    }
}
