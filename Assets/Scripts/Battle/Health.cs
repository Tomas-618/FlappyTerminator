using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable<float>
{
    [SerializeField, Min(1)] private float _maxValue;

    private float _value;

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

    public void TakeDamage(in float value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(value.ToString());

        _value -= value;
    }
}
