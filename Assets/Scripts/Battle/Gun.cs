using System.Collections;
using UnityEngine;
using Pool;

public class Gun : MonoBehaviour
{
    [SerializeField, Min(0)] private float _shootingDelay;
    [SerializeField, Min(0)] private int _maxBulletsCount;

    [SerializeField] private Transform _muzzle;
    [SerializeField] private BulletsFabric _bulletsFabric;

    private Transform _transform;
    private Coroutine _coroutine;
    private ObjectsPool<Bullet> _pool;

    private void Awake()
    {
        _transform = transform;
        _pool = new ObjectsPool<Bullet>(_bulletsFabric.Create, _maxBulletsCount);
    }

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

    private void Update() =>
        Shoot(_shootingDelay);

    private void Shoot(in float delay)
    {
        if (Input.GetKeyDown(KeyCode.F) && _coroutine == null)
        {
            _pool.PutOutEntity();
            _coroutine = StartCoroutine(WaitBeforeShoot(delay));
        }
    }

    private void ReleaseBullet(Bullet bullet)
    {
        bullet.transform.position = _muzzle.position;
        bullet.gameObject.SetActive(true);
        bullet.SetDirection(_transform.right);
    }

    private IEnumerator WaitBeforeShoot(float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        yield return wait;

        _coroutine = null;
    }
}
