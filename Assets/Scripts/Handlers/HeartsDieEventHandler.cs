using UnityEngine;
using AYellowpaper;

public abstract class HeartsDieEventHandler : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyHeartsEvents, MonoBehaviour> _events;
    //[SerializeField] private Fabric<Explosion> _explosionEffectsFabric;

    protected virtual void OnEnable() =>
        _events.Value.Died += DoActionOnDeath;

    protected virtual void OnDisable()
    {
        if (_events.Value == null)
            return;

        _events.Value.Died -= DoActionOnDeath;
    }
     
    protected abstract void DoActionOnDeath();

    //private void Die()
    //{
    //    Explosion explosion = _explosionEffectsFabric.Create();

    //    explosion.transform.position = _transform.position;
    //    explosion.gameObject.SetActive(true);
    //}
}
