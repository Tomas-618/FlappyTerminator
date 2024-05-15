using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShootingExplosionSpriteReseter : MonoBehaviour
{
    [SerializeField] private Sprite _default;

    private SpriteRenderer _view;

    private void Awake() =>
        _view = GetComponent<SpriteRenderer>();

    private void OnDisable() =>
        _view.sprite = _default;
}
