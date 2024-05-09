using System;
using System.Collections;
using UnityEngine;
using Pool;

public class Gun : MonoBehaviour, IReadOnlyGunEvents, IShootable
{
    [SerializeField, Min(0)] private float _shootingDelay;
    [SerializeField, Min(0)] private int _maxBulletsCount;

    [SerializeField] private Transform _muzzle;
    [SerializeField] private BulletsFabric _bulletsFabric;

    private ObjectsPool<Bullet> _pool;
    private Coroutine _coroutine;
    private Vector2 _direction;

    public event Action Shooted;

    private void Awake() =>
        _pool = new ObjectsPool<Bullet>(_bulletsFabric.Create, _maxBulletsCount);

    private void OnEnable()
    {
        foreach (Bullet bullet in _pool.Entities)
            bullet.Handler.Hitted += _pool.PutInEntity;

        _pool.PutIn += bullet => bullet.gameObject.SetActive(false);
        _pool.PutOut += ReleaseBullet;
        _pool.Removed += bullet => Destroy(bullet.gameObject);
    }

    private void OnDisable()
    {
        foreach (Bullet bullet in _pool.Entities)
            bullet.Handler.Hitted -= _pool.PutInEntity;

        _pool.PutIn -= bullet => bullet.gameObject.SetActive(false);
        _pool.PutOut -= ReleaseBullet;
        _pool.Removed -= bullet => Destroy(bullet.gameObject);
    }

    public void Shoot(in Vector2 direction)
    {
        if (_coroutine != null)
            return;

        _direction = direction;
        _pool.PutOutEntity();

        _coroutine = StartCoroutine(WaitBeforeShoot(_shootingDelay));
        Shooted?.Invoke();
    }

    private void ReleaseBullet(Bullet bullet)
    {
        bullet.transform.position = _muzzle.position;
        bullet.gameObject.SetActive(true);
        bullet.SetDirection(_direction);
    }

    private IEnumerator WaitBeforeShoot(float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        yield return wait;

        _coroutine = null;
    }
}
