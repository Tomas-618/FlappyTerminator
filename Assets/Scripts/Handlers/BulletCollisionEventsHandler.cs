using System;
using UnityEngine;

public class BulletCollisionEventsHandler : MonoBehaviour
{
    [SerializeField] private CollisionInfo _collision;
    [SerializeField] private BulletMediator _mediator;

    public event Action<Bullet> Hitted;

    private void OnEnable() =>
        _collision.CollisionDetected += CheckCollider;

    private void OnDisable() =>
        _collision.CollisionDetected -= CheckCollider;

    private void CheckCollider(Component component)
    {
        Hitted?.Invoke(_mediator.BulletInfo);
    }
}
