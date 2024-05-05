using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdMover : MonoBehaviour, IReadOnlyBirdMoverEvents, IReadOnlyBirdMoverRigidbody2D
{
    [SerializeField, Min(0)] private float _flutteredForce;
    [SerializeField, Min(0)] private float _rotationSpeed;

    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;

    private Transform _transform;
    private Rigidbody2D _rigidbody2D;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    public event Action Fluttered;

    public Rigidbody2D Rigidbody2DInfo => _rigidbody2D;

    private void OnValidate()
    {
        if (_minRotationZ > _maxRotationZ)
            _minRotationZ = _maxRotationZ;
    }

    private void Start()
    {
        _transform = transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _maxRotation = Quaternion.Euler(Vector3.forward * _maxRotationZ);
        _minRotation = Quaternion.Euler(Vector3.forward * _minRotationZ);
    }

    private void Update()
    {
        Flutter();
        RotateOnFalling();
    }

    private void Flutter()
    {
        if (Input.GetKeyDown(KeyCode.Space) == false)
            return;

        _rigidbody2D.velocity = Vector3.up * _flutteredForce;
        _transform.rotation = _maxRotation;
        Fluttered?.Invoke();
    }

    private void RotateOnFalling() =>
        _transform.rotation = Quaternion.Lerp(_transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
}
