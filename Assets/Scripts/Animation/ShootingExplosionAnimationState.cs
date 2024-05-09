using UnityEngine;
using AYellowpaper;

[RequireComponent(typeof(Animator))]
public class ShootingExplosionAnimationState : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyGunEvents, MonoBehaviour> _gunEvents;

    private Animator _animator;

    private void OnEnable() =>
        _gunEvents.Value.Shooted += SetTrigger;

    private void OnDisable() =>
        _gunEvents.Value.Shooted += SetTrigger;

    private void Start() =>
        _animator = GetComponent<Animator>();

    private void SetTrigger() =>
        _animator.SetTrigger(ShootingExplosionAnimationParams.Shooted);
}
