using System;
using UnityEngine;

public class RobotSpawnPoint : MonoBehaviour, IInitializable<IReadOnlyHealthEvents>
{
    private IReadOnlyHealthEvents _healthEvents;
    private bool _isFree;

    public bool IsFree => _isFree;

    private void Awake() =>
        _isFree = true;

    private void OnEnable() =>
        AddListenerOnDieEvent(_healthEvents);

    private void OnDisable() =>
        RemoveListenerOnDieEvent(_healthEvents);

    public void Init(IReadOnlyHealthEvents healthEvents)
    {
        _healthEvents = healthEvents ?? throw new ArgumentNullException(nameof(healthEvents));

        AddListenerOnDieEvent(healthEvents);
    }

    private void AddListenerOnDieEvent(IReadOnlyHealthEvents healthEvents)
    {
        if (healthEvents == null)
            return;

        healthEvents.Died += ChangeState;
    }

    private void RemoveListenerOnDieEvent(IReadOnlyHealthEvents healthEvents)
    {
        if (healthEvents == null)
            return;

        healthEvents.Died -= ChangeState;
    }

    private void ChangeState() =>
        _isFree = !_isFree;
}
