using UnityEngine;
using AYellowpaper;

[RequireComponent(typeof(Animator))]
public class BirdAnimationState : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyBirdMoverEvents, MonoBehaviour> _moverEvents;

    private Animator _animator;

    public IReadOnlyBirdMoverEvents MoverEvents => _moverEvents.Value;

    private void OnEnable() =>
        MoverEvents.Fluttered += SetFlutterParameter;

    private void OnDisable() =>
        MoverEvents.Fluttered -= SetFlutterParameter;

    private void Start() =>
        _animator = GetComponent<Animator>();

    private void SetFlutterParameter() =>
        _animator.SetTrigger(BirdAnimationParams.Fluttered);
}
