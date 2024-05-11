using System.Collections;
using UnityEngine;
using AYellowpaper;

[RequireComponent(typeof(Animator))]
public class BirdAnimationState : MonoBehaviour
{
    [SerializeField, Min(0)] private float _damageDelay;

    [SerializeField] private InterfaceReference<IReadOnlyBirdMoverEvents, MonoBehaviour> _moverEvents;
    [SerializeField] private InterfaceReference<IReadOnlyHeartsEvents, MonoBehaviour> _heartsEvents;

    private Animator _animator;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _moverEvents.Value.Fluttered += SetFlutterParameter;
        _heartsEvents.Value.Damaged += SetDamageParameter;
    }

    private void OnDisable()
    {
        _moverEvents.Value.Fluttered -= SetFlutterParameter;
        _heartsEvents.Value.Damaged -= SetDamageParameter;
    }

    private void Start() =>
        _animator = GetComponent<Animator>();

    private void SetFlutterParameter() =>
        _animator.SetTrigger(BirdAnimationParams.Fluttered);

    private void SetDamageParameter(int count)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SetDamageBoolForSpecifiedTime(_damageDelay));
    }

    private IEnumerator SetDamageBoolForSpecifiedTime(float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        _animator.SetBool(BirdAnimationParams.IsDamaged, true);

        yield return wait;

        _animator.SetBool(BirdAnimationParams.IsDamaged, false);
    }
}
