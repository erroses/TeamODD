using System;
using UnityEngine;

namespace GameJam.Project.Test
{
    public class MapTest : MonoBehaviour, IGameSystem
    {
        public GameObject cubePrefab;
        public int cubeCount;
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
            for (var i = 0; i < cubeCount; i++)
            {
                if (cubes[i])
                {
                    Destroy(cubes[i]);
                }
            }
        }

        public void Replace()
        {
            for (var i = 0; i < cubes.Length; i++)
            {
                if (cubes[i])
                {
                    Destroy(cubes[i]);
                    cubes[i] = null;
                }
            }
            SpawnEntities();
        }

        private void Awake()
        {
            if (!cubePrefab)
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
            var positions = _entityPlaceGenerator.Generate(cubeCount);
            for (var i = 0; i < positions.Length; i++)
            {
                var position = positions[i];
                if (!position.HasValue)
                {
                    continue;
                }
                var value = position.Value;
                value.y = 1.5f;
                var o = Instantiate(cubePrefab, value, Quaternion.identity);
                o.transform.SetParent(transform, false);
                o.SetActive(true);
                o.name += $"-{i}";
                cubes[i] = o;
            }
        }
    }
}
