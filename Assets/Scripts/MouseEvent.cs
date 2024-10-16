using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouseEvent : MonoBehaviour
{
    Transform[] child = new Transform[5];
    GameObject[] childObject = new GameObject[5];
    public GameObject Option;
    public GameObject MainMenu;
    public Slider volumeSlider;

    private void Start()
    {
        Transform firstChild = transform.GetChild(0);

        for (int i = 0; i < 5; i++)
        {
            child[i] = firstChild.GetChild(i);
            childObject[i] = child[i].gameObject;
        }
    }

    private void OnMouseOver()
    {
        child[0].gameObject.SetActive(false);

        bool check = Input.GetMouseButton(0);
        for (int i = 1; i < 5; i++)
        {
            childObject[i].SetActive((i < 3) ? check : !check);
        }
    }

    private void OnMouseExit()
    {
        childObject[0].SetActive(true);
        for (int i = 1; i < 5; i++)
        {
            childObject[i].SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        childObject[0].SetActive(true);
        for (int i = 1; i < 5; i++)
        {
            childObject[i].SetActive(false);
        }

        switch (name)
        {
            case "StartButton":
                SelectStart();
                break;
            case "OptionButton":
                SelectOption();
                break;
            case "ExitButton":
                SelectExit();
                break;
        }
    }

    public void SelectStart()
    {
        SceneManager.LoadScene("Introduction");
    }

    public void SelectOption()
    {
        MainMenu.SetActive(false);
        Option.SetActive(true);
        volumeSlider.gameObject.SetActive(true);
    }

    public void SelectExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
