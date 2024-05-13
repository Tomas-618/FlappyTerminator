using UnityEngine;
using AYellowpaper;

public class BirdDieEventHandler : HeartsDieEventHandler
{
    [SerializeField] private InterfaceReference<ICanOnlyDisable, MonoBehaviour> _input;
    [SerializeField] private Bird _entity;

    protected override void DoActionOnDeath()
    {
        _input.Value.Disable();
        _entity.gameObject.SetActive(false);
    }
}
