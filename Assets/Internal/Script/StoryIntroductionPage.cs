using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryIntroductionPage : MonoBehaviour
{
    private bool initialized;
    private Coroutine delay;

    private void Awake()
    {
        initialized = false;
    }

    private void Start()
    {
        delay = StartCoroutine(Delay(1f));
    }

    private void Update()
    {
        if (!initialized)
        {
            return;
        }
        if (Input.anyKeyDown)
        {
            LoadGameScene();
        }
    }

    public void OnMouseDown()
    {
        if (!initialized)
        {
            return;
        }
        LoadGameScene();
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("GameTest");
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        initialized = true;
    }
}
