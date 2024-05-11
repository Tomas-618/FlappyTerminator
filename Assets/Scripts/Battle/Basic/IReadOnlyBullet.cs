public interface IReadOnlyBullet
{
    BulletCollisionEventsHandler Handler { get; }

    float Damage { get; }
}
