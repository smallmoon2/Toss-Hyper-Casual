using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    [SerializeField] GameObject settingsUI;
    [SerializeField] bool muteAudioOnPause = true;  // �� �� �ɼ����� on/off
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
        Debug.Log("���� Ȱ��ȭ");
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

        if (muteAudioOnPause) AudioListener.pause = true;   // �� ����� ��ü �Ͻ�����
    }

    public void Resume()
    {
        if (!IsPaused) return;
        IsPaused = false;
        Time.timeScale = 1f;
        foreach (var s in gameplayInputScripts) if (s) s.enabled = true;

        if (muteAudioOnPause) AudioListener.pause = false;  // �� ����� �簳
    }
}
