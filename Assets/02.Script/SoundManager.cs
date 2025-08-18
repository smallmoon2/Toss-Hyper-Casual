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
    private AudioSource loopSource;  // 루프 전용


    public bool SfxEnabled { get; private set; } = true;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);

        bgmSource = gameObject.AddComponent<AudioSource>(); bgmSource.loop = true;
        loopSource = gameObject.AddComponent<AudioSource>(); loopSource.loop = true;
        sfxSource = gameObject.AddComponent<AudioSource>(); sfxSource.loop = false;

        soundDict = new Dictionary<string, Sound>();
        foreach (var s in sounds)
            if (!soundDict.ContainsKey(s.name)) soundDict.Add(s.name, s);

        //  초기값: 항상 소리 켜짐
        SetSfxEnabled(true);
    }

    void Start()
    {
        PlayBGM("BlueMushBGM");
    }

    public void Play(string soundName)
    {
        if (!SfxEnabled) return;
        if (soundDict.TryGetValue(soundName, out var sound))
            sfxSource.PlayOneShot(sound.clip, sound.volume);
    }

    public void PlayLoop(string soundName)
    {
        if (!SfxEnabled) return;
        if (loopSource.isPlaying && soundName == currentLoopSound) return;

        if (soundDict.TryGetValue(soundName, out var sound))
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
        if (soundDict.TryGetValue(soundName, out var sound))
        {
            bgmSource.clip = sound.clip;
            bgmSource.volume = sound.volume;
            bgmSource.Play();
        }
        else Debug.LogWarning($"BGM '{soundName}' not found!");
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = Mathf.Clamp01(volume);
    }

    // 효과음 + 루프 + BGM 모두 함께 켜고/끄기
    public void SetSfxEnabled(bool enabled)
    {
        SfxEnabled = enabled;
        sfxSource.mute = !enabled;
        loopSource.mute = !enabled;
        bgmSource.mute = !enabled;  // BGM까지 묶어서 음소거
    }

    public void ToggleSfx()
    {
        SetSfxEnabled(!SfxEnabled);
    }

    public void StopAllSfxKeepBgm()
    {
        // 루프 SFX 정지
        if (loopSource)
        {
            loopSource.Stop();
            loopSource.clip = null;
        }
        currentLoopSound = null;

        // 원샷 SFX 강제 정지 (PlayOneShot까지 확실히 끊기 위해 enable 토글)
        if (sfxSource)
        {
            sfxSource.enabled = false;  // 모든 재생 강제 중단
            sfxSource.enabled = true;   // 즉시 복구 (다음 SFX 재생 가능)
        }
    }
}
