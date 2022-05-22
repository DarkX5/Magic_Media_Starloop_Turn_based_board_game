using TurnBased.Core;
using UnityEngine;

namespace TurnBased.Audio
{
    public class AudioController : MonoBehaviour
    {
        public static AudioController Instance { get; private set; }
        [Header("Settings")]
        [SerializeField] private AudioClip[] gameClips = null;
        [SerializeField] private AudioClip[] victoryClips = null;
        [SerializeField] private AudioClip[] defeatClips = null;
        [SerializeField] private AudioClip[] footstepClips = null;

        [Header("Auto-Set - visible 4 debug")]
        [SerializeField] private AudioSource audioSource = null;

        private float normalVolume = 0.25f;
        private float muteVolume = 0f;

        private void Awake() {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            if (audioSource == null) {
                audioSource = GetComponent<AudioSource>();
            }
            if (audioSource == null) {
                audioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
                Debug.LogWarning("Audio Source SHOULD be set -> a backup audio Source has been created on the Audio Controller object");
            }
            // setup audio source
            audioSource.playOnAwake = true;
            audioSource.loop = true;
            audioSource.volume = normalVolume;
            audioSource.enabled = false;

            // start a random gameplay music clip
            audioSource.clip = gameClips[Random.Range(0, gameClips.Length)];
            audioSource.enabled = true;
            
            GameHandler.onPlayerVictory += PlayVictoryClip;
            GameHandler.onPlayerDefeat += PlayDefeatClip;
            AudioTogglerEvent.onSoundToggle += ToggleSound;
        }
        private void OnDestroy() {
            AudioTogglerEvent.onSoundToggle -= ToggleSound;
            GameHandler.onPlayerVictory -= PlayVictoryClip;
            GameHandler.onPlayerDefeat -= PlayDefeatClip;
        }

        private void ToggleSound(bool isSoundOn) {
            if(isSoundOn)
                audioSource.volume = normalVolume;
            else
                audioSource.volume = muteVolume;
        }

        private void PlayVictoryClip() {
            audioSource.enabled = false;
            // disable clip looping
            audioSource.loop = false;
            audioSource.clip = victoryClips[Random.Range(0, victoryClips.Length)];
            audioSource.enabled = true;
        }
        private void PlayDefeatClip() {
            audioSource.enabled = false;
            // disable clip looping
            audioSource.loop = false;
            audioSource.clip = defeatClips[Random.Range(0, defeatClips.Length)];
            audioSource.enabled = true;
        }

        public AudioClip GetFootstepClip() {
            return footstepClips[Random.Range(0, footstepClips.Length)];
        }
    }
}