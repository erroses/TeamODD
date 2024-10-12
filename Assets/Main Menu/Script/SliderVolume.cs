using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    public Slider volumeSlider; // Inspector���� �Ҵ�
    public AudioManager audioManager;
    void Start()
    {
        volumeSlider.value = 0.5f;

        // audioManager�� �ν��Ͻ��� ���� SetVolume �޼��带 ȣ���մϴ�.
        volumeSlider.onValueChanged.AddListener(audioManager.SetVolume);
    }
}
