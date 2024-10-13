using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    public Slider volumeSlider; // Inspector���� �Ҵ�

    void Start()
    {
        // AudioManager�� �̱��� �ν��Ͻ��� �����ͼ� �ʱ� ����

        volumeSlider.value = 0.5f;
        volumeSlider.onValueChanged.AddListener(AudioManager.Instance.SetVolume);
    }
}