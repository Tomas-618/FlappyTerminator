using UnityEngine;
using AYellowpaper;

public class RobotDieEventHandler : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyHealthEvents, MonoBehaviour> _events;
    [SerializeField] private InterfaceReference<ICanOnlyPutOutInPosition, MonoBehaviour> _pool;

    private Transform _transform;

    private void Awake() =>
        _transform = transform;

    private void OnEnable() =>
        _events.Value.Died += Die;

    private void OnDisable() =>
        _events.Value.Died -= Die;

    private void Die() =>
        _pool.Value.PutOutInPosition(_transform.position);
}
