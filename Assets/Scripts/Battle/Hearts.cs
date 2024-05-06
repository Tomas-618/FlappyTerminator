using System;
using UnityEngine;

public class Hearts : MonoBehaviour, IKillable, IReadOnlyHeartsEvents
{
    [SerializeField, Min(1)] private int _initialCount;

    private int _count;

    public event Action<int> Damaged;

    public event Action Died;

    private void Start() =>
        _count = _initialCount;

    public void Kill() =>
        SetCount(0);

    public void TakeDamage() =>
        SetCount(_count - 1);

    private void SetCount(in int count)
    {
        _count = Mathf.Clamp(count, 0, _initialCount);

        if (_count > 0)
        {
            Damaged?.Invoke(_count);

            return;
        }
        
        Died?.Invoke();
    }
}
