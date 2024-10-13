
using UnityEngine;

public class GameStatistics : MonoBehaviour
{
    private static GameObject _instance;
    private static GameStatistics _statistics;


    [RuntimeInitializeOnLoadMethod]
    public static void InitializeInstance()
    {
        if (FindAnyObjectByType<GameStatistics>() == null)
        {
            _instance = new GameObject("GameStatistics", typeof(GameStatistics));
            DontDestroyOnLoad(_instance);
            _statistics = _instance.GetComponent<GameStatistics>();
        }
    }

    public static GameStatistics Instance => _statistics;

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
