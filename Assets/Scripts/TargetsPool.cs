using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetsPool: Singleton<TargetsPool>
{
    [SerializeField] private List<Target> targetPrefabs;
        
    private readonly Dictionary<TargetType, Target> _targetsPrefabsDict = new Dictionary<TargetType, Target>();
    private readonly List<Target> _targetsPool = new List<Target>();

    public Target GetTargetFromPool(TargetType targetType, Transform spawnPosition)
    {
        Target targetToSpawn = null;

        foreach (var target in _targetsPool.Where(target => targetType == target.TargetType))
        {
            target.gameObject.transform.position = spawnPosition.position;
            target.gameObject.SetActive(true);
            targetToSpawn = target; 
            break;
        }

        if (targetToSpawn is null)
        {
            targetToSpawn =  Instantiate(_targetsPrefabsDict[targetType]);

            targetToSpawn.transform.position = spawnPosition.position;
        }

        _targetsPool.Remove(targetToSpawn);

        return targetToSpawn;
    }

    public void ReturnTargetToPool(Target target)
    {
        target.gameObject.SetActive(false);
        _targetsPool.Add(target);
    }

    private void OnEnable()
    {
        foreach (var targetPrefab in targetPrefabs)
        {
            _targetsPrefabsDict[targetPrefab.TargetType] = targetPrefab;
        }
    }
}