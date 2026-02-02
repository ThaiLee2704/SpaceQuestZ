using UnityEngine;

public class PhaserBullet : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(PhaserWeapon.Instant.speed * Time.deltaTime, 0f);

        if (transform.position.x > 9)
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CONSTANT.TAG_OBSTACLE))
            gameObject.SetActive(false);
    }
}
