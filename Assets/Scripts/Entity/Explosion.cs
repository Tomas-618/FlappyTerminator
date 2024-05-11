using System;
using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField, Min(0)] private float _lifeTime;

    private Coroutine _coroutine;

    public event Action<Explosion> LifeTimeEnded;

    private void OnEnable() =>
        _coroutine = StartCoroutine(ProcessLifeTime(_lifeTime));

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator ProcessLifeTime(float value)
    {
        WaitForSeconds wait = new WaitForSeconds(value);

        yield return wait;

        LifeTimeEnded?.Invoke(this);
    }
}
