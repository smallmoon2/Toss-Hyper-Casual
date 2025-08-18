using UnityEngine;
using UnityEngine.EventSystems;

public class OpenSettingsOnPointerDown : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        PauseManager.Instance?.OpenSettings();
    }
}