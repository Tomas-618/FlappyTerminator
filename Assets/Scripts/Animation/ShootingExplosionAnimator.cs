using UnityEngine;
using AYellowpaper;

[RequireComponent(typeof(Animator))]
public class ShootingExplosionAnimator : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyGunEvents, MonoBehaviour> _gunEvents;

    private Animator _animator;

    private void OnEnable() =>
        _gunEvents.Value.Shooted += SetTrigger;

    private void OnDisable()
    {
        if (_gunEvents.Value == null)
            return;

        _gunEvents.Value.Shooted += SetTrigger;
    }

    private void Start() =>
        _animator = GetComponent<Animator>();

    private void SetTrigger() =>
        _animator.Play(ShootingExplosionAnimatorStates.Explode);
}
