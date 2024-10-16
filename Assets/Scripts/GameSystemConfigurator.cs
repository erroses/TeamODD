using UnityEngine;

namespace GameJam.Project
{
    public class GameSystemConfigurator : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            Application.targetFrameRate = 60;
            Time.fixedDeltaTime = 0.015625f;
            Time.timeScale = 1.0f;
        }
    }
}
