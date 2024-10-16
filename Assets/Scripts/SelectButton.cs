using GameJam.Project;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{
    AudioManager audioManager;

    public void SelectStart()
    {
        Scene scene = SceneManager.GetSceneByName("Introduction");
        Debug.Log($"{scene == null}");
        SceneManager.LoadScene("Introduction");
    }

    public void SelectIntroduction()
    {
        Scene scene = SceneManager.GetSceneByName("Introduction");
        Debug.Log($"{scene == null}");
        // SceneManager.LoadScene("Introduction");
    }

    public void SelectMain()
    {
        audioManager = AudioManager.Instance;
        audioManager.AudioPause(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
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
