using UnityEngine;

public class PhaserBullet : MonoBehaviour
{
    private int bulletDamage;
    private float bulletSpeed;

    public void Setup(int damage, float speed)
    {
        this.bulletDamage = damage;
        this.bulletSpeed = speed;
    }

    void Update()
    {
        transform.position += new Vector3(bulletSpeed * Time.deltaTime, 0f);

        if (transform.position.x > 9)
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageableTarget))
        {
            damageableTarget.TakeDamage(bulletDamage, this.gameObject.tag);

            this.gameObject.SetActive(false);
        }
    }
}
