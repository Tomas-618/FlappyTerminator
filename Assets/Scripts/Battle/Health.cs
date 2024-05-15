using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable<float>, IReadOnlyHealthEvents
{
    [SerializeField, Min(1)] private float _maxValue;

    private float _value;

    public event Action<float> Damaged;

    public event Action Died;

    public event Action Reset;

    public float Value
    {
        get => _value;
        private set
        {
            if (value >= _maxValue)
                _value = _maxValue;
            else if (value <= 0)
                _value = 0;
            else
                _value = value;
        }
    }

    private void OnEnable()
    {
        _value = _maxValue;
        Reset?.Invoke();
    }

    public void TakeDamage(in float value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(value.ToString());

        Value -= value;

        if (Value <= 0)
        {
            Died?.Invoke();

            return;
        }

        Damaged?.Invoke(Value);
    }
}
