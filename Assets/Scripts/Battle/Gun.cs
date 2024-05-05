using UnityEngine;
using Pool;

public class Gun : MonoBehaviour
{
    [SerializeField, Min(0)] private int _maxBulletsCount;

    [SerializeField] private Transform _muzzle;
    [SerializeField] private BulletsFabric _bulletsFabric;

    private Transform _transform;
    private ObjectsPool<Bullet> _pool;

    private void Awake()
    {
        _transform = transform;
        _pool = new ObjectsPool<Bullet>(_bulletsFabric.Create, _maxBulletsCount);
    }

    private void OnEnable()
    {
        _pool.PutIn += bullet => bullet.gameObject.SetActive(false);
        _pool.PutOut += ReleaseBullet;
        _pool.Removed += bullet => Destroy(bullet.gameObject);
    }

    private void OnDisable()
    {
        _pool.PutIn -= bullet => bullet.gameObject.SetActive(false);
        _pool.PutOut -= ReleaseBullet;
        _pool.Removed -= bullet => Destroy(bullet.gameObject);
    }

    private void Update() =>
        Shoot();

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.F))
            _pool.PutOutEntity();
    }

    private void ReleaseBullet(Bullet bullet)
    {
        bullet.transform.position = _muzzle.position;
        bullet.gameObject.SetActive(true);
        bullet.SetDirection(_transform.right);
    }
}
