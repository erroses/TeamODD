using UnityEngine.Events;

public class PotObjectData : IEntity
{
    private int _healthPoint;

    public PotObjectData(long id, string name, int healthPoint)
    {
        Id = id;
        Name = name;
        _healthPoint = healthPoint;
    }

    public long Id { get; }

    public string Name { get; }

    public int HealthPoint
    {
        get
        {
            return _healthPoint;
        }
        set
        {
            int previous = _healthPoint;
            _healthPoint = value;
            OnHealthPointChange.Invoke(previous, _healthPoint);
        }
    }

    public UnityEvent<int, int> OnHealthPointChange { get; }
}
