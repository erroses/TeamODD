using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EntityPlacer : IEntityPlacer
{
    private readonly Dictionary<long, EntityPlacementData> _entities;

    public EntityPlacer()
    {
        _entities = new Dictionary<long, EntityPlacementData>();
    }

    public EntityPlacer(Dictionary<long, EntityPlacementData> entities)
    {
        _entities = entities;
    }

    public void Place(IEntity entity, Vector3 position)
    {
        if (_entities.TryGetValue(entity.Id, out EntityPlacementData data))
        {
            data.Position = position;
            return;
        }
        data = new EntityPlacementData(entity, position);
        _entities[entity.Id] = data;
    }

    public IEntity this[long id]
    {
        get
        {
            EntityPlacementData data = _entities.GetValueOrDefault(id);
            if (data == null)
            {
                return null;
            }
            return data.Entity;
        }
    }

    public void Remove(long id)
    {
        _entities.Remove(id);
    }

    public IEnumerator<(IEntity, Vector3)> GetEnumerator()
    {
        foreach (EntityPlacementData data in _entities.Values)
        {
            yield return (data.Entity, data.Position);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public class EntityPlacementData
    {
        public EntityPlacementData(IEntity entity, Vector3 position)
        {
            Entity = entity;
            Position = position;
        }

        public IEntity Entity { get; }

        public Vector3 Position { get; set; }
    }
}
