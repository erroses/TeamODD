using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameJam.Project
{
    public class SceneLoader : MonoBehaviour
    {
        private static void LoadScene(string sceneName, LoadSceneMode mode)
        {
            SceneManager.LoadScene(sceneName, mode);
        }

        public static SceneLoader Instance { get; private set; }

        private void Awake()
        {
            if (Instance)
            {
                Destroy(this);
            }
            else
            {
                DontDestroyOnLoad(this);
                Instance = this;
            }
        }

        public void LoadScene(string sceneName)
        {
            LoadScene(sceneName, LoadSceneMode.Single);
        }

        public void LoadSceneAdditive(string sceneName)
        {
            LoadScene(sceneName, LoadSceneMode.Additive);
        }

        public void UnloadScene()
        {
            UnloadScene(SceneManager.GetActiveScene().name);
        }

        public void UnloadScene(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
