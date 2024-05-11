using UnityEngine;

public class Fabric : MonoBehaviour
{
    [SerializeField] private Bullet _entity;
    [SerializeField] private Transform _parent;

    public Bullet Create()
    {
        Bullet entity = Instantiate(_entity);

        entity.transform.SetParent(_parent);

        return entity;
    }
}
