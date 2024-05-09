using UnityEngine;
using AYellowpaper;

[RequireComponent(typeof(AudioSource))]
public class GunSounds : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyGunEvents, MonoBehaviour> _events;
    [SerializeField] private AudioClip _shootingClip;

    private AudioSource _source;

    private void OnEnable() =>
        _events.Value.Shooted += PlayOnShoot;

    private void OnDisable() =>
        _events.Value.Shooted -= PlayOnShoot;

    private void Start() =>
        _source = GetComponent<AudioSource>();

    private void PlayOnShoot()
    {
        _source.clip = _shootingClip;
        _source.Play();
    }
}
