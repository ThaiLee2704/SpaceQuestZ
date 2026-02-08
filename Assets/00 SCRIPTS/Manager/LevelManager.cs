using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private ObstaclesSpawner obstaclesSpawner;

    public int critterCounter;
    [SerializeField] private GameObject boss1;

    private void Update()
    {
        if (critterCounter >= 20)
        {
            critterCounter = 0;
            obstaclesSpawner.SpawnBoss(boss1);
        }
    }
}
