using UnityEngine;
using AYellowpaper;

public class RobotDieEventHandler : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyHealthEvents, MonoBehaviour> _events;
    [SerializeField] private ExplosionEffectsPool _pool;

    private Transform _transform;

    private void Awake() =>
        _transform = transform;

    private void OnEnable() =>
        _events.Value.Died += Die;

    private void OnDisable() =>
        _events.Value.Died -= Die;

    private void Die() =>
        _pool.PutOutInPosition(_transform.position);
}
