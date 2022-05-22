using System.Collections;
using System.Collections.Generic;
using TurnBased.Audio;
using UnityEngine;

public class CharacterAudioEvents : MonoBehaviour
{
    [Header("Auto-Set - visible 4 debug")]
    [SerializeField] private AudioSource audioSource = null;
    private void Start() {
        if (audioSource == null) {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null) {
                audioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
            }
        }

        audioSource.playOnAwake = true;
        audioSource.loop = false;
        audioSource.volume = 1f;
        audioSource.enabled = false;
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
