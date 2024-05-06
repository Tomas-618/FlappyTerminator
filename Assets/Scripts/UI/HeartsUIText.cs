using TMPro;
using UnityEngine;
using AYellowpaper;

public class HeartsUIText : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyHeartsEvents, MonoBehaviour> _heartsEvents;
    [SerializeField] private TMP_Text _view;
    [SerializeField] private string _standartText;

    private void OnEnable()
    {
        _heartsEvents.Value.Damaged += ChangeHPValue;
        _heartsEvents.Value.Died += SetHPValueOnDie;
    }

    private void OnDisable()
    {
        _heartsEvents.Value.Damaged -= ChangeHPValue;
        _heartsEvents.Value.Died -= SetHPValueOnDie;
    }

    private void ChangeHPValue(int count) =>
        _view.text = count + _standartText;

    private void SetHPValueOnDie() =>
        _view.text = 0 + _standartText;
}
