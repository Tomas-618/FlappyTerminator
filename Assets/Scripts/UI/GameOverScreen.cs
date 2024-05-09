using UnityEngine;
using AYellowpaper;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyHeartsEvents, MonoBehaviour> _heartsEvents;

    private CanvasGroup _view;

    private void OnEnable() =>
        _heartsEvents.Value.Died += Unlock;

    private void OnDisable()
    {
        if (_heartsEvents.Value == null)
            return;

        _heartsEvents.Value.Died -= Unlock;
    }

    private void Start() =>
        _view = GetComponent<CanvasGroup>();

    private void Unlock()
    {
        _view.alpha = 1;
        _view.interactable = true;
    }
}
