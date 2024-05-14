using System;
using UnityEngine;
using AYellowpaper;

public class RobotDieEventHandler : MonoBehaviour, IInitializable<ICanOnlyPutOutInPosition>
{
    [SerializeField] private InterfaceReference<IReadOnlyHealthEvents, MonoBehaviour> _events;

    private ICanOnlyPutOutInPosition _pool;
    private Transform _transform;

    private void Awake() =>
        _transform = transform;

    private void OnEnable() =>
        _events.Value.Died += Die;

    private void OnDisable() =>
        _events.Value.Died -= Die;

    public void Init(ICanOnlyPutOutInPosition pool) =>
        _pool = pool ?? throw new ArgumentNullException(nameof(pool));

    private void Die() =>
        _pool.PutOutInPosition(_transform.position);
}
