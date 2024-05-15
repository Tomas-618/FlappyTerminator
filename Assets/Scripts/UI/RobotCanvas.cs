using UnityEngine;
using AYellowpaper;

[RequireComponent(typeof(Canvas))]
public class RobotCanvas : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyHealthEvents, MonoBehaviour> _model;
    [SerializeField] private InterfaceReference<IReadOnlySliderHiderEvents, MonoBehaviour> _sliderHider;
    [SerializeField] private Transform _target;

    private Transform _transform;

    private void Awake() =>
        _transform = transform;

    private void OnEnable()
    {
        _model.Value.Died += RemoveParent;
        _sliderHider.Value.AlphaSetToZero += SetParentAndPosition;
    }

    private void OnDisable()
    {
        if (_model.Value == null)
            return;

        _model.Value.Died -= RemoveParent;
        _sliderHider.Value.AlphaSetToZero -= SetParentAndPosition;
    }

    private void RemoveParent() =>
        _transform.SetParent(null);

    private void SetParentAndPosition()
    {
        _transform.SetParent(_target);
        _transform.position = _target.position;
    }
}
