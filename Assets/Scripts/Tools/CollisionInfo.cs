using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionInfo : MonoBehaviour
{
    public event Action<Component> CollisionDetected;

    private void OnValidate() =>
        GetComponent<Collider2D>().isTrigger = true;

    private void OnTriggerEnter2D(Collider2D other) =>
        CollisionDetected?.Invoke(other.transform);
}
