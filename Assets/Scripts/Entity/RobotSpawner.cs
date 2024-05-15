using System;
using System.Collections;
using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    [SerializeField, Min(0)] private float _despawnDelay;

    private Transform _transform;
    private Robot _entity;
    private bool _isBusy;

    public event Action<bool> ChangedState;

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
        ChangedState?.Invoke(true);
    }

    private void Despawn()
    {
        if (_isBusy == false)
            return;

        StartCoroutine(ProcessDespawn(_entity, _despawnDelay));
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

    private IEnumerator ProcessDespawn(Robot entity, float delay)
    {
        yield return new WaitForSeconds(delay);

        _isBusy = false;
        ChangedState?.Invoke(false);
        entity.gameObject.SetActive(false);
    }
}
