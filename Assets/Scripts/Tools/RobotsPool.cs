using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pool;

public class RobotsPool : MonoBehaviour
{
    [SerializeField, Min(0)] private float _minSpawnDelay;
    [SerializeField, Min(0)] private float _maxSpawnDelay;
    [SerializeField, Min(0)] private int _count;

    [SerializeField] private Fabric<Robot> _fabric;
    [SerializeField] private RobotSpawnPointsParent _spawnPointsParent;

    private ObjectsPool<Robot> _entities;
    private IReadOnlyList<RobotSpawnPoint> _spawnPoints;
    private Coroutine _coroutine;

    private void Awake()
    {
        _entities = new ObjectsPool<Robot>(_fabric.Create, _count);
        _spawnPointsParent.Init(_entities.AllEntities);
        _spawnPoints = _spawnPointsParent.Children;
    }

    private void OnEnable()
    {
        foreach (Robot entity in _entities.AllEntities)
        {
            entity.HealthEvents.Died += () => _entities.PutInEntity(entity);
            entity.HealthEvents.Died += SpawnAtRandomPoint;
        }

        _entities.PutIn += entity => entity.gameObject.SetActive(false);
        _entities.PutOut += entity => entity.gameObject.SetActive(true);
        _entities.Removed += entity => Destroy(entity.gameObject);
    }

    private void OnDisable()
    {
        foreach (Robot entity in _entities.AllEntities)
        {
            entity.HealthEvents.Died -= () => _entities.PutInEntity(entity);
            entity.HealthEvents.Died -= SpawnAtRandomPoint;
        }

        _entities.PutIn -= entity => entity.gameObject.SetActive(false);
        _entities.PutOut -= entity => entity.gameObject.SetActive(true);
        _entities.Removed -= entity => Destroy(entity.gameObject);
    }

    private void Start() =>
        SpawnAtRandomPoint();

    private void SpawnAtRandomPoint()
    {
        if (_coroutine != null)
            return;

        _coroutine = StartCoroutine(ProcessSpawningAtRandomPoints(_minSpawnDelay, _maxSpawnDelay));
    }

    private IEnumerator ProcessSpawningAtRandomPoints(float minDelay, float maxDelay)
    {
        WaitForSeconds wait;

        while (_spawnPoints
            .Any(spawnPoint => spawnPoint.IsFree))
        {
            RobotSpawnPoint[] freeSpawnPoints = _spawnPoints
            .Where(spawnPoint => spawnPoint.IsFree)
            .ToArray();

            Robot entity = _entities.PutOutEntity();

            int spawnPointIndex = Random.Range(0, freeSpawnPoints.Length);

            entity.transform.position = freeSpawnPoints[spawnPointIndex].transform.position;

            wait = new WaitForSeconds(Random.Range(minDelay, maxDelay));

            yield return wait;
        }

        _coroutine = null;
    }
}
