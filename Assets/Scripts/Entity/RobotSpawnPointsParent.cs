using System;
using System.Collections.Generic;
using UnityEngine;

public class RobotSpawnPointsParent : MonoBehaviour, IInitializable<IReadOnlyCollection<Robot>>
{
    [SerializeField] private RobotSpawnPoint[] _children;

    public IReadOnlyList<RobotSpawnPoint> Children => _children;

    public void Init(IReadOnlyCollection<Robot> entities)
    {
        if ((entities ?? throw new ArgumentNullException(nameof(entities))).Count != _children.Length)
            throw new InvalidOperationException($"parameter {nameof(entities)} should have " +
                $"{nameof(entities.Count)} that equal to {nameof(_children)} {nameof(_children.Length)}");

        int currentSpawnPointIndex = 0;

        foreach (Robot robot in entities)
            _children[currentSpawnPointIndex++].Init(robot.HealthEvents);
    }
}
