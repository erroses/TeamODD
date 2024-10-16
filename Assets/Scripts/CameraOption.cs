using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraOption : MonoBehaviour
{
    public Slider volumeSlider;
    public GameObject Option;
    void Start()
    {
        volumeSlider.gameObject.SetActive(true);
        Option.SetActive(false);
    }
}
