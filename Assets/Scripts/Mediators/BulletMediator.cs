using UnityEngine;

public class BulletMediator : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    public Bullet BulletInfo => _bullet;
}
