using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameOption : MonoBehaviour
{
    public GameObject cameraObject;
    public GameObject creator;
    public GameObject Option;
    public GameObject MainOption;
    public Slider volumeSlider;
    void OnMouseDown()
    {
        volumeSlider.gameObject.SetActive(false);
        switch (this.name)
        {
            case "������": GameExit(); break;
            case "����": Creator(); break;
        }
    }

    void GameExit()
    {
        cameraObject.SetActive(false);
        Option.SetActive(true);
    }

    void Creator()
    {
        creator.SetActive(true);
        MainOption.SetActive(false);
    }
}
