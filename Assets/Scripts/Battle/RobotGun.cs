using UnityEngine;
using AYellowpaper;

[RequireComponent(typeof(Gun))]
public class RobotGun : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyHealthEvents, MonoBehaviour> _healthEvents;
    [SerializeField] private EnemyShooter _shooter;
    [SerializeField] private Transform _placement;

    private Transform _transform;

    private void Awake() =>
        _transform = transform;

    private void OnEnable()
    {
        _healthEvents.Value.Died += RemoveParent;
        _healthEvents.Value.Reset += SetParentAndPosition;
    }

    private void OnDisable()
    {
        if (_healthEvents.Value == null)
            return;

        _healthEvents.Value.Died -= RemoveParent;
        _healthEvents.Value.Reset -= SetParentAndPosition;
    }

    private void RemoveParent()
    {
        _shooter.enabled = false;
        _transform.SetParent(null);
    }

    private void SetParentAndPosition()
    {
        _shooter.enabled = true;
        _transform.SetParent(_placement);
        _transform.position = _placement.position;
    }
}
