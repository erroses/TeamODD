using UnityEngine.Events;

public class PlayerData : IEntity
{
    private int _damageCount;
    private int _attackCount;
    private int _destroyCount;

    public PlayerData(long id, string name)
    {
        Id = id;
        Name = name;
        _damageCount = 0;
        _attackCount = 0;
        _destroyCount = 0;
        OnDamageCountIncrease = new UnityEvent<int>();
        OnAttackCountIncrease = new UnityEvent<int>();
        OnDestroyCountIncrease = new UnityEvent<int>();
    }

    public long Id { get; }

    public string Name { get; }

    public int DamageCount
    {
        get
        {
            return _damageCount;
        }
        set
        {
            _damageCount = value;
            OnDamageCountIncrease.Invoke(_damageCount);
        }
    }

    public int AttackCount
    {
        get
        {
            return _attackCount;
        }
        set
        {
            _attackCount = value;
            OnAttackCountIncrease.Invoke(_attackCount);
        }
    }

    public int DestroyCount
    {
        get
        {
            return _destroyCount;
        }
        set
        {
            _destroyCount = value;
            OnDestroyCountIncrease.Invoke(_destroyCount);
        }
    }

    public UnityEvent<int> OnDamageCountIncrease { get; }

    public UnityEvent<int> OnAttackCountIncrease { get; }

    public UnityEvent<int> OnDestroyCountIncrease { get; }
}
