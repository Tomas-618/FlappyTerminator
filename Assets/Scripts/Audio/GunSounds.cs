using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GunSounds : MonoBehaviour
{
    [SerializeField] private Gun _entity;
    [SerializeField] private AudioClip _shootingClip;

    private AudioSource _source;

    private void OnEnable() =>
        _entity.Shooted += PlayOnShoot;

    private void OnDisable() =>
        _entity.Shooted -= PlayOnShoot;

    private void Start() =>
        _source = GetComponent<AudioSource>();

    private void PlayOnShoot()
    {
        _source.clip = _shootingClip;
        _source.Play();
    }
}
