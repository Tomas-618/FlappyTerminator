using UnityEngine;
using AYellowpaper;

[RequireComponent(typeof(Animator))]
public class BirdHeartsImageUIAnimationState : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyHeartsEvents, MonoBehaviour> _heartsEvents;

    private Animator _animator;

    private void Start() =>
        _animator = GetComponent<Animator>();

    private void OnEnable()
    {
        _heartsEvents.Value.Damaged += SetDamageParameter;
        _heartsEvents.Value.Died += SetDieParameter;
    }

    private void OnDisable()
    {
        _heartsEvents.Value.Damaged -= SetDamageParameter;
        _heartsEvents.Value.Died -= SetDieParameter;
    }

    private void SetDamageParameter(int count) =>
        _animator.SetTrigger(BirdHeartsImageUIAnimationParams.Damaged);

    private void SetDieParameter() =>
        _animator.SetTrigger(BirdHeartsImageUIAnimationParams.Died);
}
