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

    private void OnDestroy() =>
        ExplodeInPosition(_transform.position);

    public void Init(ExplosionEffectsPool explosionEffects) =>
        _explosionEffects = explosionEffects ?? throw new ArgumentNullException(nameof(explosionEffects));

    private void CheckCollider(Component component)
    {
        if (component.GetComponent<Bullet>())
            return;

        Hitted?.Invoke(_mediator.BulletInfo);

        if (component.GetComponent<Zone>())
            return;

        ExplodeInPosition(_transform.position);
    }

    private void ExplodeInPosition(in Vector2 point) =>
        _explosionEffects.PutOutInPosition(point);
}
