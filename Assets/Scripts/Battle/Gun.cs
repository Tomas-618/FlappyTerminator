using System;
using System.Collections;
using UnityEngine;
using Pool;

public class Gun : MonoBehaviour, IReadOnlyGunEvents, IShootable, IInitializable<Fabric<Bullet>>
{
    [SerializeField, Min(0)] private float _shootingDelay;
    [SerializeField, Min(0)] private int _maxBulletsCount;

    [SerializeField] private Transform _muzzle;
    [SerializeField] private Fabric<Bullet> _bulletsFabric;

    private ObjectsPool<Bullet> _pool;
    private Coroutine _coroutine;
    private Vector2 _direction;

    public event Action Shooted;

    private void Awake() =>
        _pool = new ObjectsPool<Bullet>(_bulletsFabric.Create, _maxBulletsCount);

    private void OnEnable()
    {
        foreach (Bullet bullet in _pool.AllEntities)
            bullet.Handler.Hitted += _pool.PutInEntity;

        _pool.PutIn += PutInPool;
        _pool.PutOut += ReleaseBullet;
        _pool.Removed += DestroyBullets;
    }

    private void OnDisable()
    {
        _pool.PutInAllNonstoredEntities();

        foreach (Bullet bullet in _pool.AllEntities)
            bullet.Handler.Hitted -= _pool.PutInEntity;

        _pool.PutIn -= PutInPool;
        _pool.PutOut -= ReleaseBullet;
        _pool.Removed -= DestroyBullets;
    }

    public void Init(Fabric<Bullet> fabric) =>
        _bulletsFabric = fabric ?? throw new ArgumentOutOfRangeException(nameof(fabric));

    public void ClearPool() =>
        _pool.ClearAllEntities();

    public void Shoot(in Vector2 direction)
    {
        if (_coroutine != null)
            return;

        _direction = direction;
        _pool.PutOutEntity();

        _coroutine = StartCoroutine(WaitBeforeShoot(_shootingDelay));
        Shooted?.Invoke();
    }

    private void PutInPool(Bullet bullet)
    {
        if (bullet == null)
            return;

        bullet.gameObject.SetActive(false);
    }

    private void ReleaseBullet(Bullet bullet)
    {
        if (bullet == null)
            return;

        bullet.transform.position = _muzzle.position;
        bullet.gameObject.SetActive(true);
        bullet.SetDirection(_direction);
    }

    private void DestroyBullets(Bullet bullet)
    {
        if (bullet == null)
            return;

        if (bullet.gameObject.activeSelf)
            bullet.Explode();

        Destroy(bullet.gameObject);
    }

    private IEnumerator WaitBeforeShoot(float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        yield return wait;

        _coroutine = null;
    }
}
