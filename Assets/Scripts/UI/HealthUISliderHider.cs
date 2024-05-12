using System.Collections;
using UnityEngine;
using AYellowpaper;

public class HealthUISliderHider : MonoBehaviour
{
    [SerializeField, Min(0)] private float _changingDeltaValue;

    [SerializeField] private InterfaceReference<IReadOnlyHealthUISliderEvents, MonoBehaviour> _events;
    [SerializeField] private CanvasGroup _canvasGroup;

    private void OnEnable() =>
        _events.Value.ValueSetToZero += SetValueToZero;

    private void OnDisable() =>
        _events.Value.ValueSetToZero -= SetValueToZero;

    private void SetValueToZero() =>
        StartCoroutine(ProcessValueChanging());

    private IEnumerator ProcessValueChanging()
    {
        while (_canvasGroup.alpha != 0)
        {
            _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, 0, _changingDeltaValue);

            yield return null;
        }
    }
}
