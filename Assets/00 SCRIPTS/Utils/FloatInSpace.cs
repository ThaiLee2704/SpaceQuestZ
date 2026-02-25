using UnityEngine;

public class FloatInSpace : MonoBehaviour
{
    private void Update()
    {
        float moveX = GameManager.Instant.worldSpeed * GameManager.Instant.Player.BoostSpeed * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);

        if (transform.position.x < -12f)
            gameObject.SetActive(false);
    }
}
