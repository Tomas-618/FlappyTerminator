using System;
using UnityEngine;

public class BulletCollisionEventsHandler : MonoBehaviour
{
    [SerializeField] private CollisionInfo _collision;
    [SerializeField] private BulletMediator _mediator;

    public event Action<Bullet> Hitted;

    private void OnEnable() =>
        _collision.CollisionDetected += ProcessActionsOnCollisionDetection;

    private void OnDisable() =>
        _collision.CollisionDetected -= ProcessActionsOnCollisionDetection;

    private void ProcessActionsOnCollisionDetection(Component component)
    {
        if (component.GetComponent<Bullet>())
            return;

        Hitted?.Invoke(_mediator.BulletInfo);

        if (component.GetComponent<Zone>())
            return;

        _mediator.BulletInfo.Explode();
    }   
}
