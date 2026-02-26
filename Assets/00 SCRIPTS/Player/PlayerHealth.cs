using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private PlayerController player;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    private int damage;

    private void OnEnable()
    {
        Observer.AddListener(CONSTANT.OBSERVER_PLAYERLEVELUP, OnHealthLevelUp);
    }

    private void OnDisable()
    {
        Observer.RemoveListener(CONSTANT.OBSERVER_PLAYERLEVELUP, OnHealthLevelUp);
    }

    void Start()
    {
        player = GetComponent<PlayerController>();

        damage = 5;
        health = maxHealth;
        HUDManager.Instant.UpdateHealthSlider(health, maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CONSTANT.TAG_CRITTER))
            return;
        if (collision.gameObject.TryGetComponent(out IDamageable damageableTarget))
        {
            damageableTarget.TakeDamage(this.damage, this.gameObject.tag);
        }
    }

    private void OnHealthLevelUp(object[] datas)
    {
        maxHealth++;
        health = maxHealth;
        HUDManager.Instant.UpdateHealthSlider(health, maxHealth);
    }

    public void TakeDamage(int damageAmount, string damageSourceTag)
    {
        if (damageAmount <= 0) return;

        health -= damageAmount;
        HUDManager.Instant.UpdateHealthSlider(health, maxHealth);
        //Thông báo cho các Listener như PlayerVFX,PlayerSFX là player bị tấn công
        Observer.Notify("playerHit", null);
        if (health <= 0)
        {
            player.OnDeath();

            //Thông báo cho các Listener như PlayerVFX,PlayerSFX,GameOverSence...là player đã chết
            Observer.Notify("playerDeath", null);

            this.gameObject.SetActive(false);

        }
    }
}
