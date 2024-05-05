using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ShootingExplosionAnimationState : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private Gun _gun;

    private void OnEnable() =>
        _gun.Shooted += SetTrigger;

    private void OnDisable() =>
        _gun.Shooted += SetTrigger;

    private void Start() =>
        _animator = GetComponent<Animator>();

    private void SetTrigger() =>
        _animator.SetTrigger(ShootingExplosionAnimationParams.Shooted);
}
