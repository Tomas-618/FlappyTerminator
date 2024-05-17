using System;
using System.Collections;
using UnityEngine;

public class HealthUISliderHider : MonoBehaviour, IReadOnlySliderHiderEvents
{
    [SerializeField, Min(0)] private float _changingDeltaValue;

    [SerializeField] private RobotHealthUI _view;
    [SerializeField] private CanvasGroup _canvasGroup;

    public event Action AlphaSetToZero;

    public void Reset()
    {
        _canvasGroup.alpha = 1;
        _view.Reset();
    }

    private void OnEnable() =>
        _view.InnieSliderInfo.ValueSetToZero += SetValueToZero;

    private void OnDisable() =>
        _view.InnieSliderInfo.ValueSetToZero -= SetValueToZero;

    private void SetValueToZero() =>
        StartCoroutine(ProcessValueChanging());

    private IEnumerator ProcessValueChanging()
    {
        while (_canvasGroup.alpha != 0)
        {
            _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, 0, _changingDeltaValue);

            yield return null;
        }

        AlphaSetToZero?.Invoke();
    }
}
