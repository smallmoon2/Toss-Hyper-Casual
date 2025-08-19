using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    [SerializeField] GameObject settingsUI;
    [SerializeField] bool muteAudioOnPause = true;  // ← 이 옵션으로 on/off
    [SerializeField] float resumeDelay = 0.1f;

    public bool isResetting = false;

    public bool IsPaused { get; private set; }
    Coroutine closeRoutine;
    [SerializeField] MonoBehaviour[] gameplayInputScripts;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OpenSettings()
    {
        Debug.Log("리셋 활성화");
        Pause();
        isResetting = true;
        if (settingsUI != null) settingsUI.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsUI != null) settingsUI.SetActive(false);
        if (closeRoutine != null) StopCoroutine(closeRoutine);
        closeRoutine = StartCoroutine(CloseAfterDelay(resumeDelay));
    }

    IEnumerator CloseAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Resume();
        closeRoutine = null;
    }

    public void ToggleSettings()
    {
        if (IsPaused) CloseSettings();
        else OpenSettings();
    }

    public void Pause()
    {
        if (IsPaused) return;
        IsPaused = true;
        Time.timeScale = 0f;
        foreach (var s in gameplayInputScripts) if (s) s.enabled = false;

        if (muteAudioOnPause) AudioListener.pause = true;   // ★ 오디오 전체 일시정지
    }

    public void Resume()
    {
        if (!IsPaused) return;
        IsPaused = false;
        Time.timeScale = 1f;
        foreach (var s in gameplayInputScripts) if (s) s.enabled = true;

        if (muteAudioOnPause) AudioListener.pause = false;  // ★ 오디오 재개
    }
}
