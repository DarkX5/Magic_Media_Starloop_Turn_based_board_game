using TurnBased.Audio;
using UnityEngine;

public class CharacterAudioEvents : MonoBehaviour
{
    [Header("Auto-Set - visible 4 debug")]
    [SerializeField] private AudioSource audioSource = null;

    private float normalVolume = 1f;
    private float muteVolume = 0f;
    
    private void Start() {
        if (audioSource == null) {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null) {
                audioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
            }
        }

        audioSource.playOnAwake = true;
        audioSource.loop = false;
        audioSource.volume = normalVolume;
        audioSource.enabled = false;

        AudioTogglerEvent.onSoundToggle += ToggleSound;
    }
    private void OnDestroy() {
        AudioTogglerEvent.onSoundToggle -= ToggleSound;
    }

    private void ToggleSound(bool isSoundOn)
    {
        if (isSoundOn)
            audioSource.volume = normalVolume;
        else
            audioSource.volume = muteVolume;
    }
    public void PlayFootstep() {
        if (AudioController.Instance == null) {
            Debug.LogError("Audio Controller does not exist - clips missing. ");
            return;
        }

        audioSource.enabled = false;
        audioSource.clip = AudioController.Instance.GetFootstepClip();
        audioSource.enabled = true;
    }
}
