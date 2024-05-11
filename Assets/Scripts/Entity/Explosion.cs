using System;
using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField, Min(0)] private float _lifeTime;

    public event Action<Explosion> LifeTimeEnded;

    private void Start() =>
        StartCoroutine(ProcessLifeTime(_lifeTime));

    private IEnumerator ProcessLifeTime(float value)
    {
        WaitForSeconds wait = new WaitForSeconds(value);

        yield return wait;

        LifeTimeEnded?.Invoke(this);
    }
}
