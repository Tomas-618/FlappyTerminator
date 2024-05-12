using UnityEngine;
using AYellowpaper;

public class RobotCollisionEventsHandler : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IDamagable<float>, MonoBehaviour> _hearts;
    [SerializeField] private CollisionInfo _collision;

    private void OnEnable() =>
        _collision.CollisionDetected += TakeDamageOnCollisionDetection;

    private void OnDisable() =>
        _collision.CollisionDetected -= TakeDamageOnCollisionDetection;

    private void TakeDamageOnCollisionDetection(Component component)
    {
        if (component.TryGetComponent(out IReadOnlyBullet bullet))
            _hearts.Value.TakeDamage(bullet.Damage);
    }
}
