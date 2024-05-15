using System;
using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    private Transform _transform;
    private Robot _entity;
    private bool _isBusy;

    public IReadOnlyHealthEvents HealthEvents => _entity.HealthEvents;

    public bool IsBusy => _isBusy;

    private void Awake() =>
        _transform = transform;

    private void OnEnable() =>
        AddListenerOnDieEvent(_entity, Despawn);

    private void OnDisable() =>
        RemoveListenerOnDieEvent(_entity, Despawn);

    public void Init(Robot entity)
    {
        _entity = entity ?? throw new ArgumentNullException(nameof(entity));

        AddListenerOnDieEvent(_entity, Despawn);
    }

    public void Spawn()
    {
        if (_isBusy)
            return;

        _entity.transform.position = _transform.position;
        _entity.gameObject.SetActive(true);

        _isBusy = true;
    }

    private void Despawn()
    {
        if (_isBusy == false)
            return;

        _entity.gameObject.SetActive(false);

        _isBusy = false;
    }

    private void AddListenerOnDieEvent(Robot entity, Action died)
    {
        if (entity == null)
            return;

        entity.HealthEvents.Died += died;
    }

    private void RemoveListenerOnDieEvent(Robot entity, Action died)
    {
        if (entity == null)
            return;

        entity.HealthEvents.Died -= died;
    }


}
