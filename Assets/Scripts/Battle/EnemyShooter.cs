using System.Collections;
using UnityEngine;
using AYellowpaper;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField, Min(0)] private float _minShootingDelay;
    [SerializeField, Min(0)] private float _maxShootingDelay;

    [SerializeField] private InterfaceReference<IShootable, MonoBehaviour> _gun;

    private void OnValidate()
    {
        if (_minShootingDelay >= _maxShootingDelay)
            _minShootingDelay = _maxShootingDelay - 1;
    }

    private void OnEnable() =>
        StartCoroutine(Shoot(_minShootingDelay, _maxShootingDelay));

    private IEnumerator Shoot(float minDelay, float maxDelay)
    {
        WaitForSeconds wait;

        while (enabled)
        {
            wait = new WaitForSeconds(Random.Range(minDelay, maxDelay));

            yield return wait;

            _gun.Value.Shoot(Vector2.left);
        }
    }
}
