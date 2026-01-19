using System;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject prefabs;
    public float spawnTimer;
    public float spawnInterval;
    public int objectsPerWave;
    public int spawnedObjectCount;
}
