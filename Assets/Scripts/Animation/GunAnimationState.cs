using UnityEngine;
using AYellowpaper;

[RequireComponent(typeof(Animator))]
public class GunAnimationState : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyHeartsEvents, MonoBehaviour> _heartsEvents;

    private Animator _animator;

    private void OnEnable() =>
        _heartsEvents.Value.Died += SetFallingParameter;

    private void OnDisable()
    {
        if (_heartsEvents.Value == null)
            return;

        _heartsEvents.Value.Died -= SetFallingParameter;
    }

    private void Start() =>
        _animator = GetComponent<Animator>();

    private void SetFallingParameter() =>
        _animator.SetTrigger(GunAnimationParams.BirdExploded);
}
