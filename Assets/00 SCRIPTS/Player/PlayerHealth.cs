using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerController player;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    void Start()
    {
        player = GetComponent<PlayerController>();

        health = maxHealth;
        UIManager.Instant.UpdateHealthSlider(health, maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
            TakeDamage(1);
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        UIManager.Instant.UpdateHealthSlider(health, maxHealth);
        if (health <= 0)
        {
            player.OnDeath();

            //Thông báo cho các Listener như PlayerVFX,PlayerSFX,GameOverSence...là player đã chết
            Observer.Notify("playerDeath", null);

            this.gameObject.SetActive(false);

        }
    }
}
