using UnityEngine;
using AYellowpaper;

[RequireComponent(typeof(AudioSource))]
public class BirdSounds : HeartsDieEventHandler
{
    [SerializeField, Range(0, 1)] private float _flutterClipVolume;
    [SerializeField, Range(0, 1)] private float _damageClipVolume;

    [SerializeField] private InterfaceReference<IReadOnlyBirdMoverEvents, MonoBehaviour> _moverEvents;
    [SerializeField] private InterfaceReference<IReadOnlyHeartsEvents, MonoBehaviour> _heartsEvents;
    [SerializeField] private AudioClip _flutterClip;
    [SerializeField] private AudioClip _damageClip;

    private Transform _transform;
    private AudioSource _source;

    private void Reset()
    {
        _flutterClipVolume = 1;
        _damageClipVolume = 0.4f;
    }

    private void Awake() =>
        _transform = transform;

    protected override void OnEnable()
    {
        base.OnEnable();

        _moverEvents.Value.Fluttered += PlayOnFlutter;
        _heartsEvents.Value.Damaged += PlayOnTakingDamage;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (_moverEvents.Value == null || _heartsEvents.Value == null)
            return;

        _moverEvents.Value.Fluttered -= PlayOnFlutter;
    }

    private void Start() =>
        _source = GetComponent<AudioSource>();

    protected override void DoActionOnDeath() =>
        _transform.SetParent(null);

    private void PlayOnFlutter() =>
        PlayClip(_flutterClip, _flutterClipVolume);

    private void PlayOnTakingDamage(int count) =>
        PlayClip(_damageClip, _damageClipVolume);

    private void PlayClip(AudioClip clip, float volume)
    {
        _source.clip = clip;
        _source.PlayOneShot(clip, volume);
    }
}
