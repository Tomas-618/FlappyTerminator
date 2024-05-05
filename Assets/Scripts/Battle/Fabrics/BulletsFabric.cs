using UnityEngine;

public class BulletsFabric : MonoBehaviour
{
    [SerializeField] private Bullet _entity;
    [SerializeField] private Transform _bulletsParent;

    public Bullet Create()
    {
        Bullet bullet = Instantiate(_entity);

        bullet.transform.SetParent(_bulletsParent);

        return bullet;
    }
}
