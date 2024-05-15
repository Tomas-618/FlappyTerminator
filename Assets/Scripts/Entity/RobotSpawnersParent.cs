using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RobotSpawnersParent : MonoBehaviour
{
    [SerializeField, Min(0)] private float _minDelay;
    [SerializeField, Min(0)] private float _maxDelay;

    [SerializeField] private List<RobotSpawner> _children;
    [SerializeField] private Fabric<Robot> _fabric;

    private Coroutine _coroutine;

    private void OnValidate()
    {
        if (_minDelay >= _maxDelay)
            _minDelay = _maxDelay - 1;
    }

    private void Awake()
    {
        foreach (RobotSpawner child in _children)
        {
            Robot robot = _fabric.Create();

            child.Init(robot);
        }
    }

    private void OnEnable() =>
        _children.ForEach(child => child.HealthEvents.Died += StartSpawningInRandomChild);

    private void OnDisable() =>
        _children.ForEach(child => child.HealthEvents.Died -= StartSpawningInRandomChild);

    private void Start() =>
        StartSpawningInRandomChild();

    private void StartSpawningInRandomChild()
    {
        if (_coroutine != null)
            return;

        _coroutine = StartCoroutine(ProcessSpawning(_minDelay, _maxDelay));
    }

    private IEnumerator ProcessSpawning(float minDelay, float maxDelay)
    {
        WaitForSeconds wait;

        while (_children
            .Any(IsSpawnerPointFree))
        {
            RobotSpawner[] childrenWithFreePoints = _children
                .Where(IsSpawnerPointFree)
                .ToArray();

            wait = new WaitForSeconds(Random.Range(minDelay, maxDelay));
            childrenWithFreePoints[Random.Range(0, childrenWithFreePoints.Length)].Spawn();

            yield return wait;
        }

        _coroutine = null;
    }

    private bool IsSpawnerPointFree(RobotSpawner child) =>
        child.IsBusy == false;
}
