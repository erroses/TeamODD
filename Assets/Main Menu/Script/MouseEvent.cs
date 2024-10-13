using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseEvent : MonoBehaviour
{
    Transform[] child = new Transform[5];
    GameObject[] childObject = new GameObject[5];
    public GameObject Option;
    public GameObject MainMenu;

    private void Start()
    {
        Transform firstChild = transform.GetChild(0);

        for (int i = 0; i < 5; i++) {
            child[i] = firstChild.GetChild(i);
            childObject[i] = child[i].gameObject;
        }
    }

    // 마우스가 오브젝트에 들어왔을 때 실행되는 함수
    void OnMouseOver()
    {
        child[0].gameObject.SetActive(false);

        bool check = Input.GetMouseButton(0);
        for (int i = 1; i < 5; i++)
        {
            childObject[i].SetActive((i < 3) ? check : !check);
        }
    }

    // 마우스가 오브젝트에서 나갔을 때 실행되는 함수
    void OnMouseExit()
    {
        childObject[0].SetActive(true);
        for (int i = 1; i < 5; i++)
        {
            childObject[i].SetActive(false);
        }
        // 여기서 원하는 이벤트를 처리합니다.
    }

    void OnMouseDown()
    {
        switch(this.name)
        {
            case "StartButton": SelectStart(); break;
            case "OptionButton": SelectOption(); break;
            case "ExitButton": SelectExit(); break;
        }
    }

    public void SelectStart()
    {
        SceneManager.LoadScene("GameTest");
    }

    public void SelectOption()
    {
        MainMenu.SetActive(false);
        Option.SetActive(true);
    }

    public void SelectExit()
    {
        // 에디터에서 실행 중인지 확인 (에디터에서는 종료되지 않기 때문에 메시지를 띄움)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 실제 빌드된 게임에서 실행 시 게임을 종료
#endif
    }
}
