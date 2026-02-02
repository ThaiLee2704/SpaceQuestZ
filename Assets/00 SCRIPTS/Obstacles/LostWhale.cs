using UnityEngine;

public class LostWhale : ObstacleBase
{
    private void Update()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.Instant.OnGameWin();
        }
    }
}
