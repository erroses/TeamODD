using System;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameJam.Project
{
    public class GameSystem : MonoBehaviour, IGameSystem
    {
        private readonly TimeoutCounter _startCounter;

        private Coroutine _timerCoroutine;
        private RandomEntityPlaceGenerator _entityPlaceGenerator;

        [SerializeField] private GameObject jarObjectPrefab;
        [SerializeField] private GameObject player1Prefab;
        [SerializeField] private GameObject player2Prefab;

        [SerializeField] private GameObject player1Object;
        [SerializeField] private GameObject player2Object;

        [SerializeField] private AudioManager audioManager;

        [SerializeField] private int jarObjectCount;

        [SerializeField] private float regionRadius;
        [SerializeField] private float playerRegionRadiusOffset;
        [SerializeField] private float collisionRadius;
        [SerializeField] private GameObject[] jarObjects;
        [SerializeField] private int[] jarObjectOwners;

        [SerializeField] private ReadyPanel readyPanel;
        [SerializeField] private GameSystemTimeoutCounter timeoutCounter;

        public GameSystem(TimeoutCounterBuilder builder)
        {
            TimeoutCounter = builder();
            _startCounter = new TimeoutCounter(2, 1);
        }

        public GameSystem() : this(() => new TimeoutCounter(119f, 1f))
        {
        }

        public TimeoutCounter TimeoutCounter { get; }

        public void Initialize()
        {
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
            if (!jarObjectPrefab)
            {
                throw new NullReferenceException(nameof(jarObjectPrefab));
            }
            if (!player1Prefab)
            {
                throw new NullReferenceException(nameof(player1Prefab));
            }
            if (!player2Prefab)
            {
                throw new NullReferenceException(nameof(player2Prefab));
            }
            if (jarObjectCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(jarObjectCount));
            }

            jarObjects = new GameObject[jarObjectCount];
            jarObjectOwners = new int[jarObjectCount];
            audioManager = FindObjectOfType<AudioManager>();
            _entityPlaceGenerator = new RandomEntityPlaceGenerator(regionRadius, collisionRadius);

            _startCounter.OnTimerStart.AddListener(
                () =>
                {
                    readyPanel.gameObject.SetActive(true);
                    var offset = TimeoutCounter.Timeout + 1;
                    var minutes = ((int) offset) / 60;
                    var seconds = offset - (minutes * 60);
                    var format = $"{minutes:D2}:{seconds:00.0}";
                    timeoutCounter.SetCountdown(format);
                });
            _startCounter.OnTimerElapsed.AddListener(
                timeout =>
                {
                    Debug.Log($"Ready Counter remained: {timeout}");
                    readyPanel.SetReadyCountDown((int) (_startCounter.Timeout - timeout) + 1);
                });
            _startCounter.OnTimerEnd.AddListener(
                () =>
                {
                    readyPanel.gameObject.SetActive(false);
                    var enumerator = TimeoutCounter.StartCoroutine();
                    _timerCoroutine = StartCoroutine(enumerator);
                });

            TimeoutCounter.OnTimerStart.AddListener(
                () =>
                {
                    var player1Move = player1Object.GetComponent<MovePlayer>();
                    player1Move.moveEnabled = true;
                    var player2Move = player2Object.GetComponent<MovePlayer>();
                    player2Move.moveEnabled = true;
                    Debug.Log("Timeout Counter started.");
                });

            TimeoutCounter.OnTimerElapsed.AddListener(
                timeout =>
                {
                    var offset = TimeoutCounter.Timeout - timeout + 1;
                    var minutes = ((int) offset) / 60;
                    var seconds = offset - (minutes * 60);
                    var format = $"{minutes:D2}:{seconds:00.0}";
                    timeoutCounter.SetCountdown(format);
                });

            TimeoutCounter.OnTimerEnd.AddListener(
                () =>
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
            var positions = _entityPlaceGenerator.Generate(jarObjectCount + 2);
            for (var i = 0; i < positions.Length - 2; i++)
            {
                var position = positions[i];
                if (!position.HasValue)
                {
                    continue;
                }
                var value = position.Value;
                value.y = 3.5f;
                var jarObject = SpawnEntity(jarObjectPrefab, value, $"Jar-{i}");
                var jarState = jarObject.GetComponent<JarState>();
                jarObjects[i] = jarObject;
                jarState.jarObjectData = new JarObjectData(i, jarObject.name, i % 2 == 0 ? 0 : jarState.maxHealth);
                jarState.jarObjectData.OnHealthPointChange.AddListener(
                    (prev, next) =>
                    {
                        if (next == 0 && jarObjectOwners[jarState.jarObjectData.Id] == 0)
                        {
                            GameStatistics.Instance.Player1JarAttackCount--;
                            GameStatistics.Instance.Player2JarAttackCount++;
                            jarObjectOwners[jarState.jarObjectData.Id] = 1;
                            Debug.Log("Player2 destroyed a jar.");
                            Debug.Log(
                                $"Player1: Player2 = {GameStatistics.Instance.Player1JarAttackCount}:{GameStatistics.Instance.Player2JarAttackCount}");
                        }
                        if (next == 3 && jarObjectOwners[jarState.jarObjectData.Id] == 1)
                        {
                            GameStatistics.Instance.Player1JarAttackCount++;
                            GameStatistics.Instance.Player2JarAttackCount--;
                            jarObjectOwners[jarState.jarObjectData.Id] = 0;
                            Debug.Log("Player1 restored a jar.");
                            Debug.Log(
                                $"Player1: Player2 = {GameStatistics.Instance.Player1JarAttackCount}:{GameStatistics.Instance.Player2JarAttackCount}");
                        }
                    });
                if (i % 2 == 0)
                {
                    jarState.SetHealthPoint(0, true);
                    jarObject.name += "-broken";
                    GameStatistics.Instance.Player2JarAttackCount++;
                    jarObjectOwners[jarState.jarObjectData.Id] = 1;
                    Debug.Log(
                        $"Player1: Player2 = {GameStatistics.Instance.Player1JarAttackCount}:{GameStatistics.Instance.Player2JarAttackCount}");
                }
                else
                {
                    GameStatistics.Instance.Player1JarAttackCount++;
                    jarObjectOwners[jarState.jarObjectData.Id] = 0;
                    Debug.Log(
                        $"Player1: Player2 = {GameStatistics.Instance.Player1JarAttackCount}:{GameStatistics.Instance.Player2JarAttackCount}");
                }
            }

            var playerMoveRegion = regionRadius + playerRegionRadiusOffset;
            var player1Position = positions[jarObjectCount].Value;
            player1Object = SpawnEntity(player1Prefab, player1Position, "Player1");
            var player1Move = player1Object.GetComponent<MovePlayer>();
            player1Move.moveEnabled = false;
            player1Move.moveRegion = playerMoveRegion;

            var player2Position = positions[jarObjectCount + 1].Value;
            player2Object = SpawnEntity(player2Prefab, player2Position, "Player2");
            var player2Move = player2Object.GetComponent<MovePlayer>();
            player2Move.moveEnabled = false;
            player2Move.moveRegion = playerMoveRegion;
        }

        private GameObject SpawnEntity(GameObject prefab, Vector3 position, string name)
        {
            var o = Instantiate(prefab, position, Quaternion.identity);
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
            for (var i = 0; i < jarObjectCount; i++)
            {
                if (!jarObjects[i])
                {
                    continue;
                }
                Destroy(jarObjects[i]);
                jarObjects[i] = null;
            }
        }
    }
}
