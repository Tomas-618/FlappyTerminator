using AYellowpaper;
using UnityEngine;

public class BirdDieEventHandler : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyHeartsEvents, MonoBehaviour> _events;
    [SerializeField] private Fabric<Explosion> _explosionEffectsFabric;
    [SerializeField] private PlayerInputToShoot _input;
    [SerializeField] private BirdSounds _sounds;

    private Transform _transform;

    private void Awake() =>
        _transform = transform;

    private void OnEnable() =>
        _events.Value.Died += Die;

    private void OnDisable() =>
        _events.Value.Died -= Die;

    private void Die()
    {
        Transform gunTransform = _input.GunInfo.transform;

        Explosion explosion = _explosionEffectsFabric.Create();

        _input.enabled = false;
        _input.GunInfo.ClearPool();

        gunTransform.SetParent(null);
        gunTransform.rotation = Quaternion.identity;

        explosion.transform.position = _transform.position;
        explosion.gameObject.SetActive(true);

        _sounds.transform.SetParent(null);

        gameObject.SetActive(false);
    }
}
