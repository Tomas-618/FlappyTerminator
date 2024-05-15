using UnityEngine;
using AYellowpaper;

[RequireComponent(typeof(Canvas))]
public class RobotUITest : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyHealthEvents, MonoBehaviour> _model;
    [SerializeField] private InterfaceReference<IReadOnlySliderHiderEvents, MonoBehaviour> _sliderHider;
    [SerializeField] private Transform _target;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        _model.Value.Died += Do;
        _sliderHider.Value.AlphaSetToZero += Undo;
    }

    private void OnDisable()
    {
        if (_model.Value == null)
            return;

        _model.Value.Died -= Do;
        _sliderHider.Value.AlphaSetToZero -= Undo;
    }

    private void Do()
    {
        _transform.SetParent(null);
    }

    private void Undo()
    {
        _transform.SetParent(_target);
        _transform.position = _target.position;
    }
}
