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
    private AudioSource sfxSource;   // ���� ����
    private AudioSource loopSource;  // ���� ����


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

        //  �ʱⰪ: �׻� �Ҹ� ����
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

    // ȿ���� + ���� + BGM ��� �Բ� �Ѱ�/����
    public void SetSfxEnabled(bool enabled)
    {
        SfxEnabled = enabled;
        sfxSource.mute = !enabled;
        loopSource.mute = !enabled;
        bgmSource.mute = !enabled;  // BGM���� ��� ���Ұ�
    }

    public void ToggleSfx()
    {
        SetSfxEnabled(!SfxEnabled);
    }

    public void StopAllSfxKeepBgm()
    {
        // ���� SFX ����
        if (loopSource)
        {
            loopSource.Stop();
            loopSource.clip = null;
        }
        currentLoopSound = null;

        // ���� SFX ���� ���� (PlayOneShot���� Ȯ���� ���� ���� enable ���)
        if (sfxSource)
        {
            sfxSource.enabled = false;  // ��� ��� ���� �ߴ�
            sfxSource.enabled = true;   // ��� ���� (���� SFX ��� ����)
        }
    }
}
