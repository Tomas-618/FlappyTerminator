using UnityEngine;

public class ExplosionEffectSpawner : MonoBehaviour
{
    [SerializeField] private Explosion _explosion;

    public void Spawn() =>
        Instantiate(_explosion, transform.position, Quaternion.identity);
}
