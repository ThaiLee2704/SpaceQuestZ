using UnityEngine;

public class LostWhale : ObstacleBase
{
    private void Update()
    {
        Movement();

        if (transform.position.x < -12f)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.Instant.OnGameWin();
        }
    }
}
