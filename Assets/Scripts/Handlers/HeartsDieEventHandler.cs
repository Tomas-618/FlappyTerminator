using UnityEngine;
using AYellowpaper;

public abstract class HeartsDieEventHandler : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyHeartsEvents, MonoBehaviour> _events;

    protected virtual void OnEnable() =>
        _events.Value.Died += DoActionOnDeath;

    protected virtual void OnDisable()
    {
        if (_events.Value == null)
            return;

        _events.Value.Died -= DoActionOnDeath;
    }
     
    protected abstract void DoActionOnDeath();
}
