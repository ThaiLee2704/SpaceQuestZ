using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] private Transform minPos;
    [SerializeField] private Transform maxPos;

    [SerializeField] private int waveNumber;
    [SerializeField] private List<Wave> waves;

    //Cách 1: Nested Class: khai báo class dạng như 1 struct để dễ nhìn khi code
    [System.Serializable]
    public class Wave
    {
        public GameObject prefabs;
        public float spawnTimer;
        public float spawnInterval;
        public int objectsPerWave;
        public int spawnedObjectCount;
    }

    private void Update()
    {
        waves[waveNumber].spawnTimer += Time.deltaTime * GameManager.Instant.Player.BoostSpeed;
        if (waves[waveNumber].spawnTimer >= waves[waveNumber].spawnInterval)
        {
            waves[waveNumber].spawnTimer = 0;
            SpawnObject();
        }

        if (waves[waveNumber].spawnedObjectCount >= waves[waveNumber].objectsPerWave)
        {
            waves[waveNumber].spawnedObjectCount = 0;
            waveNumber++;
            if(waveNumber >= waves.Count)
                waveNumber = 0;
        }
    }

    private void SpawnObject()
    {
        GameObject obstacle = ObjectPooling.Instant.GetObject(waves[waveNumber].prefabs, this.transform);
        obstacle.transform.position = RandomSpawnPoint();
        obstacle.SetActive(true);
        waves[waveNumber].spawnedObjectCount++;
    }

    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;

        spawnPoint.x = minPos.position.x;
        spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);

        return spawnPoint;
    }
}
