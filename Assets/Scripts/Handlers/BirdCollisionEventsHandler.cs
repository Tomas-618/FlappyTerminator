﻿using UnityEngine;

public class BirdCollisionEventsHandler : MonoBehaviour
{
    [SerializeField] private CollisionInfo _collision;
    [SerializeField] private Hearts _hearts;

    private void OnEnable() =>
        _collision.CollisionDetected += ChangeHeartsCountOnCollisionDetection;

    private void OnDisable() =>
        _collision.CollisionDetected -= ChangeHeartsCountOnCollisionDetection;

    private void ChangeHeartsCountOnCollisionDetection(Component component)
    {
        if (component.GetComponent<Zone>())
            _hearts.Kill();
        else if (component.GetComponent<Bullet>())
            _hearts.TakeDamage();
    }
}
