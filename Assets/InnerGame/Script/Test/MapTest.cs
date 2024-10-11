using System;
using UnityEngine;

public class MapTest : MonoBehaviour, IGameSystem
{
    private readonly EntityPlacer _entityPlacer = new EntityPlacer();

    public int cubeCount;
    public GameObject cubePrefab;
    public float regionRadius;
    public float collisionRadius;
    public GameObject[] cubes;

    private RandomEntityPlaceGenerator _entityPlaceGenerator;

    public TimeoutCounter TimeoutCounter { get; } = new TimeoutCounter(60, 1f);

    public void Initialize()
    {
        SpawnEntities();
    }

    public void Dispose()
    {
        for (int i = 0; i < cubeCount; i++)
        {
            if (cubes[i] != null)
            {
                Destroy(cubes[i]);
            }
        }
    }

    public void Replace()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            if (cubes[i] != null)
            {
                Destroy(cubes[i]);
                cubes[i] = null;
            }
        }
        SpawnEntities();
    }

    private void Awake()
    {
        if (cubePrefab == null)
        {
            throw new NullReferenceException(nameof(cubePrefab));
        }
        if (cubeCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cubeCount));
        }
        cubes = new GameObject[cubeCount];
        _entityPlaceGenerator = new RandomEntityPlaceGenerator(regionRadius, collisionRadius);
    }

    private void Start()
    {
        Initialize();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    private void SpawnEntities()
    {
        _entityPlaceGenerator.RegionRadius = regionRadius;
        _entityPlaceGenerator.CollisionRadius = collisionRadius;
        Vector3?[] positions = _entityPlaceGenerator.Generate(cubeCount);
        for (int i = 0; i < positions.Length; i++)
        {
            Vector3? position = positions[i];
            if (!position.HasValue)
            {
                continue;
            }
            Vector3 value = position.Value;
            value.y = 1.5f;
            GameObject o = Instantiate(cubePrefab, value, Quaternion.identity);
            o.transform.SetParent(transform, false);
            o.SetActive(true);
            o.name += $"-{i}";
            cubes[i] = o;
        }
    }
}
