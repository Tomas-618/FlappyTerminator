using UnityEngine;

public class PlayerInputToShoot : MonoBehaviour
{
    [SerializeField] private Gun _gun;

    private Transform _gunTransform;

    private void Start() =>
        _gunTransform = _gun.transform;

    private void Update() =>
        Shoot();

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.F))
            _gun.Shoot(_gunTransform.right);
    }
}
