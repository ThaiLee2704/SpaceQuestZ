using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private ObstaclesSpawner obstaclesSpawner;
    [SerializeField] private GameObject boss1;
    public int critterCounter;

    private void Update()
    {
        if (critterCounter >= 20)
        {
            critterCounter = 0;
            obstaclesSpawner.SpawnBoss(boss1);
        }
    }
}
