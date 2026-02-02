using UnityEngine;

public abstract class ObstacleBase : MonoBehaviour
{
    protected virtual void Movement()
    {
        float moveX = GameManager.Instant.worldSpeed * GameManager.Instant.Player.BoostSpeed * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);

        if (transform.position.x < -12f)
            gameObject.SetActive(false);
    }
}
