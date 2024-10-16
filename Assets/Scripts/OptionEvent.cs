using UnityEngine;
using UnityEngine.UI;

public class OptionEvent : MonoBehaviour
{
    public GameObject Option;
    public GameObject MainMenu;
    public GameObject creator;
    public Slider volumeSlider;

    void OnMouseDown()
    {
        Option.SetActive(false);
        volumeSlider.gameObject.SetActive(false);
        switch (name)
        {
            case "나가기":
                GameExit();
                break;
            case "제작":
                Creator();
                break;
        }
    }

    void GameExit()
    {
        MainMenu.SetActive(true);
    }

    void Creator()
    {
        creator.SetActive(true);
    }
}
