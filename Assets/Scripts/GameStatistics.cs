using UnityEngine;

namespace GameJam.Project
{
    public class GameStatistics : MonoBehaviour
    {
        private static GameObject _instance;

        [RuntimeInitializeOnLoadMethod]
        public static void InitializeInstance()
        {
            var gameStatistics = FindObjectOfType<GameStatistics>();
            if (gameStatistics)
            {
                _instance = gameStatistics.gameObject;
            }
            else
            {
                _instance = new GameObject("GameStatistics", typeof(GameStatistics));
                DontDestroyOnLoad(_instance);
                Instance = _instance.GetComponent<GameStatistics>();
            }
        }

        public static GameStatistics Instance { get; private set; }

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            Player1AttackCount = 0;
            Player2AttackCount = 0;
            Player1JarAttackCount = 0;
            Player2JarAttackCount = 0;
            Debug.Log("Game Statistics initialized");
        }

        [field: SerializeField]
        public int Player1AttackCount { get; set; }

        [field: SerializeField]
        public int Player2AttackCount { get; set; }

        [field: SerializeField]
        public int Player1JarAttackCount { get; set; }

        [field: SerializeField]
        public int Player2JarAttackCount { get; set; }
    }
}
