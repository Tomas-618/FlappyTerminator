using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IReadOnlyBullet, IInitializable<ICanOnlyPutOutInPosition>
{
    private readonly float _speedCoefficient = 0.01f;

    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _minDamage;
    [SerializeField, Min(0)] private float _maxDamage;

    [SerializeField] private BulletCollisionEventsHandler _handler;

    private Transform _transform;
    private Coroutine _coroutine;
    private ICanOnlyPutOutInPosition _explosionEffects;

    public BulletCollisionEventsHandler Handler => _handler;

    public float Damage => Random.Range(_minDamage, _maxDamage);

    private void OnValidate()
    {
        if (_minDamage >= _maxDamage)
            _minDamage = _maxDamage - 1;
    }

    private void Awake() =>
        _transform = transform;

    public void Init(ICanOnlyPutOutInPosition explosionEffects) =>
        _explosionEffects = explosionEffects ?? throw new System.ArgumentNullException(nameof(explosionEffects));

    public void SetDirection(in Vector2 direction)
    {
        Vector2 move = _speedCoefficient * _speed * direction.normalized;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Move(move));
    }

    public void Explode() =>
        _explosionEffects.PutOutInPosition(transform.position);

    private IEnumerator Move(Vector2 move)
    {
        while (enabled)
        {
            _transform.Translate(move);

            yield return null;
        }
    }
}
