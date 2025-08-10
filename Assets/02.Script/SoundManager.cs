using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public List<Sound> sounds;
    private string currentLoopSound = null;

    private Dictionary<string, Sound> soundDict;
    private AudioSource audioSource;
    private AudioSource bgmSource; 

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        bgmSource = gameObject.AddComponent<AudioSource>(); 
        bgmSource.loop = true;

        soundDict = new Dictionary<string, Sound>();
        foreach (Sound s in sounds)
        {
            if (!soundDict.ContainsKey(s.name))
                soundDict.Add(s.name, s);
        }
    }

    private void Start()
    {
        SoundManager.Instance.PlayBGM("BlueMushBGM");
    }


    public void Play(string soundName)
    {
        if (soundDict.TryGetValue(soundName, out Sound sound))
            audioSource.PlayOneShot(sound.clip, sound.volume);
        else
            Debug.LogWarning($"Sound '{soundName}' not found!");
    }

    public void PlayLoop(string soundName)
    {
        if (audioSource.isPlaying && soundName == currentLoopSound) return;

        if (soundDict.TryGetValue(soundName, out Sound sound))
        {
            currentLoopSound = soundName;
            audioSource.clip = sound.clip;
            audioSource.volume = sound.volume;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
            Debug.LogWarning($"Loop sound '{soundName}' not found!");
    }

    public void Stop()
    {
        audioSource.Stop();
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
