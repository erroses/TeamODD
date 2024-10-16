using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameJam.Project.UI
{
    [RequireComponent(typeof(Slider))]
    public class SettingsSceneHandler : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        [RuntimeInitializeOnLoadMethod]
        private static void AwakeEventSystem()
        {
            var eventSystem = EventSystem.current;
            if (eventSystem)
            {
                if (!eventSystem.enabled)
                {
                    eventSystem.enabled = true;
                }
                return;
            }
            eventSystem = FindObjectOfType<EventSystem>(true);
            eventSystem.gameObject.SetActive(true);
            eventSystem.enabled = true;
            Debug.Log("Event System Initialized");
        }

        [RuntimeInitializeOnLoadMethod]
        private static void AwakeAudioListener()
        {
            var audioListener = FindObjectOfType<AudioListener>();
            if (audioListener)
            {
                if (!audioListener.enabled)
                {
                    audioListener.enabled = true;
                }
                return;
            }
            audioListener = FindObjectOfType<AudioListener>(true);
            audioListener.gameObject.SetActive(true);
            audioListener.enabled = true;
            Debug.Log("Audio Listener Initialized");
        }

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        private void Start()
        {
            var volume = AudioManager.Instance.Volume;
            slider.value = volume;
        }

        public void UpdateVolume()
        {
            var value = slider.value;
            AudioManager.Instance.Volume = value;
        }

        public void EnterCredits()
        {
            SceneLoader.Instance.LoadSceneAdditive("Credits");
        }

        public void LeaveCurrentScene()
        {
            SceneLoader.Instance.UnloadScene("Settings");
        }
    }
}
