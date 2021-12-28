using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private List<SpawnPoint> spawnPoints;

    public Target TrySpawnTarget(TargetType typeToSpawn)
    {
            var spawnPoint = GetRandomSpawnPoint();
            spawnPoint.IsAvailable = false;

            var target = TargetsPool.Instance.GetTargetFromPool(typeToSpawn, spawnPoint.PointPosition);

            target.TargetClicked += () => spawnPoint.IsAvailable = true;

            target.Stolknulsya += () => spawnPoint.IsAvailable = true;

            target.TargetFallen += obj => spawnPoint.IsAvailable = true;

            target.Spawned?.Invoke();

            return target;
    }

    private SpawnPoint GetRandomSpawnPoint()
    {
        List<SpawnPoint> AvailableSpawnPoints = new List<SpawnPoint>();
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint.IsAvailable)
            {
                AvailableSpawnPoints.Add(spawnPoint);
            }
        }
        var randomSpawnPoint = Random.Range(0, AvailableSpawnPoints.Count - 1);
        return AvailableSpawnPoints[randomSpawnPoint];
    }
}