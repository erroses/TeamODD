using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    public Slider volumeSlider; // Inspector에서 할당

    void Start()
    {
        // AudioManager의 싱글톤 인스턴스를 가져와서 초기 설정

        volumeSlider.value = AudioManager.Instance.audioSource.volume;
        volumeSlider.onValueChanged.AddListener(AudioManager.Instance.SetVolume);
    }
}