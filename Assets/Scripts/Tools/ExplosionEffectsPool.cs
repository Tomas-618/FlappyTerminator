using System.Collections;
using UnityEngine;
using Pool;

public class ExplosionEffectsPool : HeartsDieEventHandler, ICanOnlyPutOutInPosition
{
    [SerializeField, Min(0)] private int _count;
    [SerializeField, Min(0)] private float _clearingDelay;

    [SerializeField] private Fabric<Explosion> _fabric;
    [SerializeField] private bool _isClearWithDelay;

    private ObjectsPool<Explosion> _entities;
    private Coroutine _coroutine;

    private void Awake() =>
        _entities = new ObjectsPool<Explosion>(_fabric.Create, _count);

    protected override void OnEnable()
    {
        base.OnEnable();

        foreach (Explosion entity in _entities.AllEntities)
            entity.LifeTimeEnded += _entities.PutInEntity;

        _entities.PutIn += PutIn;
        _entities.PutOut += PutOut;
        _entities.Removed += Remove;
    }

    protected override void OnDisable()
    {
        _entities.PutInAllNonstoredEntities();
        base.OnDisable();

        foreach (Explosion entity in _entities.AllEntities)
            entity.LifeTimeEnded -= _entities.PutInEntity;

        _entities.PutIn -= PutIn;
        _entities.PutOut -= PutOut;
        _entities.Removed -= Remove;
    }

    public void PutOutInPosition(in Vector2 point)
    {
        Explosion explosion = _entities.PutOutEntity();

        if (explosion == null)
            return;

        explosion.transform.position = point;
    }

    protected override void DoActionOnDeath()
    {
        if (_isClearWithDelay)
            ClearWithDelay(_clearingDelay);
        else
            Clear();
    }

    private void PutIn(Explosion entity)
    {
        if (entity == null)
            return;

        entity.gameObject.SetActive(false);
    }

    private void PutOut(Explosion entity)
    {
        if (entity == null)
            return;

        entity.gameObject.SetActive(true);
    }

    private void Remove(Explosion entity)
    {
        if (entity == null)
            return;

        Destroy(entity.gameObject);
    }

    private void ClearWithDelay(in float delay)
    {
        if (_coroutine != null)
            return;

        _coroutine = StartCoroutine(WaitBeforeCleaning(_clearingDelay));
    }

    private void Clear() =>
        _entities.ClearStoredEntities();

    private IEnumerator WaitBeforeCleaning(float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        yield return wait;

        Clear();
        _coroutine = null;
    }
}
