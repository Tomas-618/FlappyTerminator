using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IReadOnlyBullet
{
    private readonly float _speedCoefficient = 0.01f;

    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _damage;

    [SerializeField] private BulletCollisionEventsHandler _handler;

    private Transform _transform;
    private Coroutine _coroutine;

    public BulletCollisionEventsHandler Handler => _handler;

    public float Damage => _damage;

    private void OnEnable() =>
        _transform = transform;

    public void Init(ExplosionEffectsPool explosionEffects) =>
        Handler.Init(explosionEffects);

    public void SetDirection(in Vector2 direction)
    {
        Vector2 move = _speedCoefficient * _speed * direction.normalized;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Move(move));
    }

    private IEnumerator Move(Vector2 move)
    {
        while (enabled)
        {
            _transform.Translate(move);

            yield return null;
        }
    }
}
