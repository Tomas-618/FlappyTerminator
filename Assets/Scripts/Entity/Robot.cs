using UnityEngine;
using AYellowpaper;

public class Robot : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IInitializable<Fabric<Bullet>>, MonoBehaviour> _gun;
    [SerializeField] private InterfaceReference<IInitializable<ICanOnlyPutOutInPosition>, MonoBehaviour> _dieEventHandler;
    [SerializeField] private InterfaceReference<IReadOnlyHealthEvents, MonoBehaviour> _healthEvents;

    public IInitializable<Fabric<Bullet>> GunInfo => _gun.Value;

    public IInitializable<ICanOnlyPutOutInPosition> DieEventHandlerInfo => _dieEventHandler.Value;

    public IReadOnlyHealthEvents HealthEvents => _healthEvents.Value;
}
