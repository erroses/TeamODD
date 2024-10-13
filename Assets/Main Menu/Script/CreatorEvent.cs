using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreatorEvent : MonoBehaviour
{
    public GameObject Option;
    public GameObject creator;
    public Slider volumeSlider;
    void OnMouseDown()
    {
        Option.SetActive(true);
        volumeSlider.gameObject.SetActive(true);
        creator.SetActive(false);
    }
}
