using UnityEngine;
using AYellowpaper;

public class RobotsFabric : Fabric<Robot>
{
    [SerializeField] private InterfaceReference<ICanOnlyPutOutInPosition, MonoBehaviour> _explosionEffectsPool;
    [SerializeField] private Fabric<Bullet> _bulletsFabric;

    public override Robot Create()
    {
        Robot entity = base.Create();

        entity.GunInfo.Init(_bulletsFabric);
        entity.DieEventHandlerInfo.Init(_explosionEffectsPool.Value);

        return entity;
    }
}
