using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GunAnimationState : HeartsDieEventHandler
{
    private Animator _animator;

    private void Start() =>
        _animator = GetComponent<Animator>();

    protected override void DoActionOnDeath() =>
        SetFallingParameter();

    private void SetFallingParameter() =>
        _animator.SetTrigger(GunAnimationParams.BirdDied);
}
