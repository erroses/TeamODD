using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    public Slider volumeSlider; // Inspector에서 할당
    public AudioManager audioManager;
    void Start()
    {
        volumeSlider.value = 0.5f;

        // audioManager의 인스턴스를 통해 SetVolume 메서드를 호출합니다.
        volumeSlider.onValueChanged.AddListener(audioManager.SetVolume);
    }
}
