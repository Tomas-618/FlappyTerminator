using System;
using UnityEngine;

public class BulletCollisionEventsHandler : MonoBehaviour
{
    [SerializeField] private CollisionInfo _collision;
    [SerializeField] private BulletMediator _mediator;

    private Transform _transform;
    private ExplosionEffectsPool _explosionEffects;

    public event Action<Bullet> Hitted;

    private void Awake() =>
        _transform = transform;

    private void OnEnable() =>
        _collision.CollisionDetected += CheckCollider;

    private void OnDisable() =>
        _collision.CollisionDetected -= CheckCollider;

    public void Init(ExplosionEffectsPool explosionEffects) =>
        _explosionEffects = explosionEffects ?? throw new ArgumentNullException(nameof(explosionEffects));

    private void CheckCollider(Component component)
    {
        if (component.GetComponent<Zone>())
        {
            Hitted?.Invoke(_mediator.BulletInfo);
        }
        else if (component.GetComponent<Bird>() || component.GetComponent<Robot>())
        {
            _explosionEffects.PutOutInPosition(_transform.position);
            Hitted?.Invoke(_mediator.BulletInfo);
        }
    }
}
