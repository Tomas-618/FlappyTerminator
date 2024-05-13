using UnityEngine;

public class ExplosionEffectSpawner : HeartsDieEventHandler
{
    [SerializeField] private Fabric<Explosion> _fabric;
    [SerializeField] private Transform _target;

    protected override void DoActionOnDeath()
    {
        Explosion explosion = _fabric.Create();

        explosion.transform.position = _target.position;
        explosion.gameObject.SetActive(true);
    }
}
