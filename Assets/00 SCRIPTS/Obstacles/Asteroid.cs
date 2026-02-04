using System.Collections;
using TreeEditor;
using UnityEngine;

public class Asteroid : ObstacleBase
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;

    private Material defaulMaterial;
    [SerializeField] private Material hitMaterial;

    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private int lives;

    private Rigidbody2D rb;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        defaulMaterial = spriteRenderer.material;
    }

    void Start()
    {
        InitAsteroid();
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CONSTANT.TAG_PLAYER) || collision.gameObject.CompareTag(CONSTANT.TAG_BULLET))
        {
            spriteRenderer.material = hitMaterial;
            AudioManager.Instant.PlayHitRockSound();
            StartCoroutine(ResetMaterial());
            lives--;
            if (lives <= 0)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
                AudioManager.Instant.PlayDestroyAsteroidSound();
                this.spriteRenderer.material = defaulMaterial;
                lives = 5;
                gameObject.SetActive(false);
            }
        }
    }
    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaulMaterial;
    }
}
