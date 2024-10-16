using GameJam.Project;

using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    public Slider volumeSlider; // Inspector���� �Ҵ�

    void Start()
    {
        // AudioManager�� �̱��� �ν��Ͻ��� �����ͼ� �ʱ� ����

        volumeSlider.value = AudioManager.Instance.audioSource.volume;
        volumeSlider.onValueChanged.AddListener(AudioManager.Instance.SetVolume);
    }
}