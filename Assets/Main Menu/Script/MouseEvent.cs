using System.Collections;
using System.Collections.Generic;
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

    // ���콺�� ������Ʈ�� ������ �� ����Ǵ� �Լ�
    void OnMouseOver()
    {
        child[0].gameObject.SetActive(false);

        bool check = Input.GetMouseButton(0);
        for (int i = 1; i < 5; i++)
        {
            childObject[i].SetActive((i < 3) ? check : !check);
        }
    }

    // ���콺�� ������Ʈ���� ������ �� ����Ǵ� �Լ�
    void OnMouseExit()
    {
        childObject[0].SetActive(true);
        for (int i = 1; i < 5; i++)
        {
            childObject[i].SetActive(false);
        }
        // ���⼭ ���ϴ� �̺�Ʈ�� ó���մϴ�.
    }

    void OnMouseDown()
    {
        childObject[0].SetActive(true);
        for (int i = 1; i < 5; i++)
        {
            childObject[i].SetActive(false);
        }

        switch (this.name)
        {
            case "StartButton": SelectStart(); break;
            case "OptionButton": SelectOption(); break;
            case "ExitButton": SelectExit(); break;
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
        // �����Ϳ��� ���� ������ Ȯ�� (�����Ϳ����� ������� �ʱ� ������ �޽����� ���)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���� ����� ���ӿ��� ���� �� ������ ����
#endif
    }
}
