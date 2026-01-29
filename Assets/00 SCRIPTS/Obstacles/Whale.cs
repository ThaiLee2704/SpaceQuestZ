using UnityEngine;

public class Whale : ObstacleBase
{
    void Update()
    {
        Movement();

        if (transform.position.x < -12f)
            Destroy(this.gameObject);
    }
}
