using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using AYellowpaper;

[RequireComponent(typeof(Slider))]
public class SmoothHealthUISlider : MonoBehaviour, IReadOnlyHealthUISliderEvents
{
    [SerializeField, Min(0)] private float _changingDeltaValue;

    [SerializeField] private InterfaceReference<IReadOnlyHealthEvents, MonoBehaviour> _events;

    private Slider _view;
    private Coroutine _coroutine;

    public event Action ValueSetToZero;

    public void Reset() =>
        _view.value = _view.maxValue;

    private void Awake() =>
        _view = GetComponent<Slider>();

    private void OnEnable()
    {
        _events.Value.Damaged += ChangeValue;
        _events.Value.Died += SetValueOnDie;
    }

    private void OnDisable()
    {
        if (_events.Value == null)
            return;

        _events.Value.Damaged -= ChangeValue;
        _events.Value.Died -= SetValueOnDie;
    }

    private void ChangeValue(float value)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ProcessValueChanging(value, _changingDeltaValue));
    }

    private void SetValueOnDie() =>
        ChangeValue(0);

    private IEnumerator ProcessValueChanging(float desiredValue, float changingDeltaValue)
    {
        while (_view.value != desiredValue)
        {
            _view.value = Mathf.MoveTowards(_view.value, desiredValue, changingDeltaValue);

            yield return null;
        }

        if (desiredValue == 0)
            ValueSetToZero?.Invoke();
    }
}
