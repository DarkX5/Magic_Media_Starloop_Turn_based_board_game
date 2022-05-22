using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioTogglerEvent : MonoBehaviour
{
    public static event Action<bool> onSoundToggle = null;

    [Header("Settings")]
    [SerializeField] private Sprite soundOnSprite = null;
    [SerializeField] private Sprite soundOffSprite = null;

    private Image buttonImage = null;

    // Start is called before the first frame update
    void Start()
    {
        if (buttonImage == null) {
            buttonImage = GetComponent<Image>();
        }
        buttonImage.sprite = soundOnSprite;
    }

    public void ToggleSound() {
        if(buttonImage.sprite.GetInstanceID() == soundOnSprite.GetInstanceID()) {
            // sound ON -> toggle off
            buttonImage.sprite = soundOffSprite;
            onSoundToggle?.Invoke(false);
        } else {
            // sound OFF -> toggle on
            buttonImage.sprite = soundOnSprite;
            onSoundToggle?.Invoke(true);
        }
    }
}
