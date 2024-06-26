﻿using UnityEngine;
using AYellowpaper;

[RequireComponent(typeof(Animator))]
public class BirdHeartsImageUIAnimator : MonoBehaviour
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
        if (_heartsEvents.Value == null)
            return;

        _heartsEvents.Value.Damaged -= SetDamageParameter;
        _heartsEvents.Value.Died -= SetDieParameter;
    }

    private void SetDamageParameter(int count) =>
        _animator.SetTrigger(BirdHeartsImageUIAnimatorParams.Damaged);

    private void SetDieParameter() =>
        _animator.SetTrigger(BirdHeartsImageUIAnimatorParams.Died);
}
