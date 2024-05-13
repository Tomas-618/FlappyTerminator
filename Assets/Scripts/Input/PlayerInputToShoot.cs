using UnityEngine;

public class PlayerInputToShoot : MonoBehaviour, ICanOnlyDisable
{
    [SerializeField] private Gun _gun;

    private Transform _gunTransform;

    private void Start() =>
        _gunTransform = _gun.transform;

    private void Update() =>
        Shoot();

    public void Disable()
    {
        _gunTransform.SetParent(null);
        _gunTransform.rotation = Quaternion.identity;

        _gun.ClearPool();
        enabled = false;
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.F))
            _gun.Shoot(_gunTransform.right);
    }
}
