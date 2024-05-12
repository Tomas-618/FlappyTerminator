using UnityEngine;
using Pool;

public class ExplosionEffectsPool : MonoBehaviour
{
    [SerializeField, Min(0)] private int _count;

    [SerializeField] private Fabric<Explosion> _fabric;

    private ObjectsPool<Explosion> _pool;

    private void Awake() =>
        _pool = new ObjectsPool<Explosion>(_fabric.Create, _count);

    private void OnEnable()
    {
        foreach (Explosion entity in _pool.Entities)
            entity.LifeTimeEnded += _pool.PutInEntity;

        _pool.PutIn += entity => entity.gameObject.SetActive(false);
        _pool.PutOut += entity => entity.gameObject.SetActive(true);
        _pool.Removed += entity => Destroy(entity.gameObject);
    }

    private void OnDisable()
    {
        _pool.PutIn -= entity => entity.gameObject.SetActive(false);
        _pool.PutOut -= entity => entity.gameObject.SetActive(true);
        _pool.Removed -= entity => Destroy(entity.gameObject);
    }

    public void PutOutInPosition(in Vector2 point)
    {
        Explosion explosion = _pool.PutOutEntity();

        if (explosion == null)
            return;

        explosion.transform.position = point;
    }
}
