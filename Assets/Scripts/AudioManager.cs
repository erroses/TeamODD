using UnityEngine;

namespace GameJam.Project
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip clip;

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            var audioManager = FindObjectOfType<AudioManager>();
            if (audioManager)
            {
                Instance = audioManager;
            }
            else
            {
                var gameObject = new GameObject("Audio Manager", typeof(AudioManager));
                DontDestroyOnLoad(gameObject);
                Instance = gameObject.GetComponent<AudioManager>();
            }
        }

        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public float Volume
        {
            get { return audioSource.volume; }
            set { audioSource.volume = value; }
        }

        public void AudioPause(bool isPaused)
        {
            audioSource.pitch = isPaused ? 0f : 1f;
        }

        public void SetVolume(float volume)
        {
            audioSource.volume = volume; // 0.0에서 1.0 사이의 값
        }
    }
}
