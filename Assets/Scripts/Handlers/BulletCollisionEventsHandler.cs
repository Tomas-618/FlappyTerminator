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
        if (component.GetComponent<Zone>() || component.GetComponent<Bird>() || component.GetComponent<Robot>())
            Hitted?.Invoke(_mediator.BulletInfo);
    }
}
