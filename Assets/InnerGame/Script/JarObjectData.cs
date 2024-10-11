using UnityEngine.Events;

public class JarObjectData : IEntity
{
    private int _healthPoint;

    public JarObjectData(long id, string name, int healthPoint)
    {
        Id = id;
        Name = name;
        _healthPoint = healthPoint;
        OnHealthPointChange = new UnityEvent<int, int>();
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
