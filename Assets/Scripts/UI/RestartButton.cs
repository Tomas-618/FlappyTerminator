using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour
{
    private Button _entity;

    private void Awake() =>
        _entity = GetComponent<Button>();

    private void OnEnable() =>
        _entity.onClick.AddListener(RestartLevel);

    private void OnDisable() =>
        _entity.onClick.RemoveListener(RestartLevel);

    private void RestartLevel() =>
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
