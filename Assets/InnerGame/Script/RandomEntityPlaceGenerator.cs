using UnityEngine;

public class RandomEntityPlaceGenerator : IEntityPlaceGenerator
{
    private float _regionRadius;
    private float _collisionRadius;

    public RandomEntityPlaceGenerator(float regionRadius, float collisionRadius)
    {
        _regionRadius = regionRadius;
        _collisionRadius = collisionRadius;
    }

    public float RegionRadius
    {
        get { return _regionRadius; }
        set { _regionRadius = value; }
    }

    public float CollisionRadius
    {
        get { return _collisionRadius; }
        set { _collisionRadius = value; }
    }

    public Vector3?[] Generate(int count)
    {
        Vector3?[] positions = new Vector3?[count];
        for (int i = 0; i < count; i++)
        {
            int trial = (int)_regionRadius;
            while (positions[i] == null && --trial >= 0)
            {
                Vector3 position = GetRandomPositionOnMesh();
                position.y = 0f;
                if (IsPositionEmpty(position, positions))
                {
                    positions[i] = position;
                    break;
                }
            }
        }
        return positions;
    }

    private bool IsPositionEmpty(Vector3 position, Vector3?[] positions)
    {
        foreach (Vector3? item in positions)
        {
            if (!item.HasValue)
            {
                continue;
            }
            if (Vector3.Distance(position, item.Value) < _collisionRadius)
            {
                return false;
            }
        }

        return true;
    }

    private Vector3 GetRandomPositionOnMesh()
    {
        return Random.insideUnitSphere * _regionRadius;
    }
}
