using UnityEngine;
using AYellowpaper;

public class BirdSounds : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyBirdMoverEvents, MonoBehaviour> _moverEvents;
    [SerializeField] private InterfaceReference<IReadOnlyHeartsEvents, MonoBehaviour> _heartsEvents;
    [SerializeField] private AudioClip _flutterClip;
    [SerializeField] private AudioClip _damageClip;
    [SerializeField] private AudioSource _flutterAudioSource;
    [SerializeField] private AudioSource _damageAudioSource;

    private void OnEnable()
    {
        _moverEvents.Value.Fluttered += PlayOnFlutter;
        _heartsEvents.Value.Damaged += PlayOnTakingDamage;
    }

    private void OnDisable()
    {
        if (_moverEvents.Value == null || _heartsEvents.Value == null)
            return;

        _moverEvents.Value.Fluttered -= PlayOnFlutter;
    }

    private void PlayOnFlutter() =>
        PlayClip(_flutterAudioSource, _flutterClip);

    private void PlayOnTakingDamage(int count) =>
        PlayClip(_damageAudioSource, _damageClip);

    private void PlayClip(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
}
