using UnityEngine;
using AYellowpaper;

[RequireComponent(typeof(AudioSource))]
public class BirdSounds : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyBirdMoverEvents, MonoBehaviour> _events;
    [SerializeField] private AudioClip _flutterClip;

    private AudioSource _source;

    private void OnEnable() =>
        _events.Value.Fluttered += PlayOnFlutter;

    private void OnDisable()
    {
        if (_events.Value == null)
            return;

        _events.Value.Fluttered -= PlayOnFlutter;
    }

    private void Start() =>
        _source = GetComponent<AudioSource>();

    private void PlayOnFlutter()
    {
        _source.clip = _flutterClip;
        _source.Play();
    }
}
