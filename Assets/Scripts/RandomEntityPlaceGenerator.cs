using UnityEngine;

public class RandomEntityPlaceGenerator : IEntityPlaceGenerator
{
    public RandomEntityPlaceGenerator(float regionRadius, float collisionRadius)
    {
        RegionRadius = regionRadius;
        CollisionRadius = collisionRadius;
    }

    public float RegionRadius { get; set; }

    public float CollisionRadius { get; set; }

    public Vector3?[] Generate(int count)
    {
        var positions = new Vector3?[count];
        for (var i = 0; i < count; i++)
        {
            var trial = (int)RegionRadius;
            while (positions[i] == null && --trial >= 0)
            {
                var position = GetRandomPositionOnMesh();
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
        foreach (var item in positions)
        {
            if (!item.HasValue)
            {
                continue;
            }
            if (Vector3.Distance(position, item.Value) < CollisionRadius)
            {
                return false;
            }
        }

        return true;
    }

    private Vector3 GetRandomPositionOnMesh()
    {
        return Random.insideUnitSphere * RegionRadius;
    }
}
