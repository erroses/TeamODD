using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate TimeoutCounter TimeoutCounterBuilder();

public class GameSystem : MonoBehaviour, IGameSystem
{
    private readonly TimeoutCounter _startCounter;

    private Coroutine _timerCoroutine;
    private RandomEntityPlaceGenerator _entityPlaceGenerator;

    [SerializeField]
    private GameObject jarObjectPrefab;
    [SerializeField]
    private GameObject player1Prefab;
    [SerializeField]
    private GameObject player2Prefab;

    [SerializeField]
    private GameObject player1Object;
    [SerializeField]
    private GameObject player2Object;


    [SerializeField]
    private AudioManager audioManager;

    [SerializeField]
    private int jarObjectCount;

    [SerializeField]
    private float regionRadius;
    [SerializeField]
    private float playerRegionRadiusOffset;
    [SerializeField]
    private float collisionRadius;
    [SerializeField]
    private GameObject[] jarObjects;

    public GameSystem(TimeoutCounterBuilder builder)
    {
        TimeoutCounter = builder();
        _startCounter = new TimeoutCounter(3, 1);
    }

    public GameSystem() : this(() => new TimeoutCounter(30f, 0.1f))
    {
    }

    public TimeoutCounter TimeoutCounter { get; }

    public void Initialize()
    {
        // IEnumerator enumerator = TimeoutCounter.StartCoroutine();
        // _timerCoroutine = StartCoroutine(enumerator);
        StartCoroutine(_startCounter.StartCoroutine());
    }

    public void Dispose()
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
        }
    }

    private void Awake()
    {
        if (jarObjectPrefab == null)
        {
            throw new NullReferenceException(nameof(jarObjectPrefab));
        }
        if (player1Prefab == null)
        {
            throw new NullReferenceException(nameof(player1Prefab));
        }
        if (player2Prefab == null)
        {
            throw new NullReferenceException(nameof(player2Prefab));
        }
        if (jarObjectCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(jarObjectCount));
        }

        jarObjects = new GameObject[jarObjectCount];
        audioManager = FindObjectOfType<AudioManager>();
        _entityPlaceGenerator = new RandomEntityPlaceGenerator(regionRadius, collisionRadius);

        _startCounter.OnTimerElapsed.AddListener(timeout =>
        {
            Debug.Log($"Ready Counter remained: {timeout}");
        });
        _startCounter.OnTimerEnd.AddListener(() =>
        {
            IEnumerator enumerator = TimeoutCounter.StartCoroutine();
            _timerCoroutine = StartCoroutine(enumerator);
        });

        TimeoutCounter.OnTimerStart.AddListener(() =>
        {
            MovePlayer player1Move = player1Object.GetComponent<MovePlayer>();
            player1Move.moveEnabled = true;
            MovePlayer player2Move = player2Object.GetComponent<MovePlayer>();
            player2Move.moveEnabled = true;
            Debug.Log("Timeout Counter started.");
        });

        TimeoutCounter.OnTimerElapsed.AddListener(timeout =>
        {
            // Debug.Log($"Timeout Counter remained: {timeout}");
        });

        TimeoutCounter.OnTimerEnd.AddListener(() =>
        {
            Debug.Log("Timeout Counter finished.");
            Debug.Log($"Player1 Attack Count: {GameStatistics.Instance.Player1AttackCount}");
            Debug.Log($"Player1 Jar Attack Count: {GameStatistics.Instance.Player1JarAttackCount}");
            Debug.Log($"Player2 Attack Count: {GameStatistics.Instance.Player2AttackCount}");
            Debug.Log($"Player2 Jar Attack Count: {GameStatistics.Instance.Player2JarAttackCount}");
            SceneManager.LoadScene("Ending");
        });
    }

    private void Start()
    {
        GameStatistics.Instance.Initialize();
        Initialize();
        SpawnEntities();
    }

    private void OnDestroy()
    {
        Dispose();
        DestroyJars();
    }

    private void SpawnEntities()
    {
        _entityPlaceGenerator.RegionRadius = regionRadius;
        _entityPlaceGenerator.CollisionRadius = collisionRadius;
        Vector3?[] positions = _entityPlaceGenerator.Generate(jarObjectCount + 2);
        for (int i = 0; i < positions.Length - 2; i++)
        {
            Vector3? position = positions[i];
            if (!position.HasValue)
            {
                continue;
            }
            Vector3 value = position.Value;
            value.y = 3.5f;
            GameObject jarObject = SpawnEntity(jarObjectPrefab, value, $"Jar-{i}");
            JarState jarState = jarObject.GetComponent<JarState>();
            jarObjects[i] = jarObject;
            GameStatistics.Instance.Player1JarAttackCount++;
            jarState.jarObjectData = new JarObjectData(i, jarObject.name, jarState.maxHealth);
            jarState.jarObjectData.OnHealthPointChange.AddListener((prev, next) =>
            {
                if (next == 0)
                {
                    GameStatistics.Instance.Player1JarAttackCount--;
                    GameStatistics.Instance.Player2JarAttackCount++;
                    jarObjects[jarState.jarObjectData.Id] = null;
                    Debug.Log("Player2 destroyed a jar.");
                }
            });
            if (i % 2 == 0)
            {
                jarState.SetHealthPoint(2);
                jarObject.name += "-broken";
            }
        }

        float playerMoveRegion = regionRadius + playerRegionRadiusOffset;
        Vector3 player1Position = positions[jarObjectCount].Value;
        player1Object = SpawnEntity(player1Prefab, player1Position, "Player1");
        MovePlayer player1Move = player1Object.GetComponent<MovePlayer>();
        player1Move.moveEnabled = false;
        player1Move.moveRegion = playerMoveRegion;

        Vector3 player2Position = positions[jarObjectCount + 1].Value;
        player2Object = SpawnEntity(player2Prefab, player2Position, "Player2");
        MovePlayer player2Move = player2Object.GetComponent<MovePlayer>();
        player2Move.moveEnabled = false;
        player2Move.moveRegion = playerMoveRegion;
    }

    private GameObject SpawnEntity(GameObject prefab, Vector3 position, string name)
    {
        GameObject o = Instantiate(prefab, position, Quaternion.identity);
        o.transform.SetParent(transform, false);
        if (!o.activeSelf)
        {
            o.SetActive(true);
        }
        o.name = name;
        return o;
    }

    private void DestroyJars()
    {
        for (int i = 0; i < jarObjectCount; i++)
        {
            if (jarObjects[i] != null)
            {
                Destroy(jarObjects[i]);
                jarObjects[i] = null;
            }
        }
    }
}
