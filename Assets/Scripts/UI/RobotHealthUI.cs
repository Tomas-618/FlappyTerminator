using UnityEngine;

public class RobotHealthUI : MonoBehaviour
{
    [SerializeField] private SmoothHealthUISlider _slider;
    [SerializeField] private SmoothHealthUISlider _innieSlider;

    public IReadOnlyHealthUISliderEvents SliderInfo => _slider;

    public IReadOnlyHealthUISliderEvents InnieSliderInfo => _innieSlider;

    public void Reset()
    {
        _slider.Reset();
        _innieSlider.Reset();
    }
}
