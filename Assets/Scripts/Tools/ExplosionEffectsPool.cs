using UnityEngine;
using Pool;

public class ExplosionEffectsPool : MonoBehaviour
{
    [SerializeField, Min(0)] private int _count;

    [SerializeField] private Fabric _fabric;

    private ObjectsPool<Explosion> _pool;

    private void Awake()
    {
        //_pool = new ObjectsPool<Explosion>(, _count);
    }
}
