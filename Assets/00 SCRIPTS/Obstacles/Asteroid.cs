using System.Collections;
using TreeEditor;
using UnityEngine;

public class Asteroid : ObstacleBase, IDamageable
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private FlashWhite flashWhite;

    private int lives;
    private int maxLives;
    private int damage;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject destroyEffect;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        flashWhite = GetComponent<FlashWhite>();
    }

    void Start()
    {
        InitAsteroid();
    }

    private void OnEnable()
    {
        lives = maxLives;
    }

    void Update()
    {
        Movement();
    }

    void InitAsteroid()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        float pushX = Random.Range(-1f, 0);
        float pushY = Random.Range(-1f, 1f);

        rb.linearVelocity = new Vector2(pushX, pushY);

        float randomScale = Random.Range(0.6f, 1f);
        transform.localScale = new Vector2(randomScale, randomScale);

        maxLives = 5;
        lives = maxLives;
        damage = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CONSTANT.TAG_BOSS) ||
            collision.gameObject.CompareTag(CONSTANT.TAG_OBSTACLE))
            return;
        
        if (collision.gameObject.TryGetComponent(out IDamageable damageableTarget))
        {
            damageableTarget.TakeDamage(this.damage, this.gameObject.tag);
        }
    }

    public void TakeDamage(int damageAmount, string damageSourceTag)
    {
        if (damageAmount <= 0) return;

        lives -= damageAmount;
        if (lives <= 0)
        {
            GameObject effect = ObjectPooling.Instant.GetObject(destroyEffect, transform.parent);
            effect.transform.position = transform.position;
            effect.transform.rotation = transform.rotation;
            effect.SetActive(true);

            AudioManager.Instant.PlayDestroyAsteroidSound();
            flashWhite.ResetMaterialAfterDisable();
            lives = 5;  //Hard code
            gameObject.SetActive(false);
        }
        else
        {
            AudioManager.Instant.PlayHitRockSound();
            flashWhite.Flash();
        }
    }

}
