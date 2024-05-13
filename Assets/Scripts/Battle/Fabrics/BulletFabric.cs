using UnityEngine;
using AYellowpaper;

public class BulletFabric : Fabric<Bullet>
{
    [SerializeField] private InterfaceReference<ICanOnlyPutOutInPosition, MonoBehaviour> _explosionEffects;

    public override Bullet Create()
    {
        Bullet entity = base.Create();

        entity.Init(_explosionEffects.Value);

        return entity;
    }
}
