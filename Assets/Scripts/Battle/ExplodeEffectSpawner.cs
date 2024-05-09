using UnityEngine;

public class ExplodeEffectSpawner : MonoBehaviour
{
    [SerializeField] private Explosion _explosion;

    public void Spawn() =>
        Instantiate(_explosion, transform.position, Quaternion.identity);
}
