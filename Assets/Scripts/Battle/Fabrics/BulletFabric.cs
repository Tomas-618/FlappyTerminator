using UnityEngine;

public class BulletFabric : Fabric<Bullet>
{
    [SerializeField] private ExplosionEffectsPool _explosionEffects;

    public override Bullet Create()
    {
        Bullet entity = base.Create();

        entity.Init(_explosionEffects);

        return entity;
    }
}
