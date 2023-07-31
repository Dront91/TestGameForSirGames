using UnityEngine;
using System;
using System.Collections.Generic;
[CreateAssetMenu]
public class LevelSettings : ScriptableObject
{
    [Serializable]
    public class Squad
    {
        public GameObject EnemyPrefab;
        public int EnemyCount;
    }
    [Serializable]
    public class Wave
    {
        public Squad[] Squads;
    }
    [SerializeField] private Wave _wave;
    public IEnumerable<(GameObject asset, int count)> EnumerateSquads()
    {
            foreach (var squad in _wave.Squads)
            {
                yield return (squad.EnemyPrefab, squad.EnemyCount);
            }
    }
}
