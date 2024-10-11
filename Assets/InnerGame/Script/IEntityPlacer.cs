using System.Collections.Generic;

using UnityEngine;

public interface IEntityPlacer : IEnumerable<(IEntity, Vector3)>
{
    public void Place(IEntity entity, Vector3 position);

    public IEntity this[long id] { get; }

    public void Remove(long id);
}
