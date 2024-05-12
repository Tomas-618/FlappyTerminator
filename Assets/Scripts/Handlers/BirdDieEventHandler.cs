using AYellowpaper;
using UnityEngine;

public class BirdDieEventHandler : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyHeartsEvents, MonoBehaviour> _events;
    [SerializeField] private ExplosionEffectSpawner _explosionEffectSpawner;
    [SerializeField] private PlayerInputToShoot _input;
    [SerializeField] private BirdSounds _sounds;

    private void OnEnable() =>
        _events.Value.Died += Die;

    private void OnDisable() =>
        _events.Value.Died -= Die;

    private void Die()
    {
        Transform gunTransform = _input.GunInfo.transform;

        _input.GunInfo.ClearPool();
        _input.enabled = false;

        gunTransform.SetParent(null);
        gunTransform.rotation = Quaternion.identity;

        _sounds.transform.SetParent(null);
        _explosionEffectSpawner.Spawn();

        gameObject.SetActive(false);
    }
}
