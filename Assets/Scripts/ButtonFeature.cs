using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFeature : MonoBehaviour
{
    public GameObject Option;
    public GameObject Normal;
    public GameObject Background;
    public Slider volumeSlider;

    public void CloseOption()
    {
        Option.SetActive(false);
        Normal.SetActive(true);
    }

    public void OpenOption()
    {
        Option.SetActive(true);
        Background.SetActive(false);
        volumeSlider.gameObject.SetActive(true);
    }
}
