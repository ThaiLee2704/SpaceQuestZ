using UnityEngine;

public class Asteroid : ObstacleBase
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    [SerializeField] private Sprite[] sprites;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        InitAsteroid();
    }

    void Update()
    {
        Movement();

        if (transform.position.x < -12f)
            Destroy(this.gameObject);
    }

    void InitAsteroid()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        float pushX = Random.Range(-1f, 0);
        float pushY = Random.Range(-1f, 1f);

        rb.linearVelocity = new Vector2(pushX, pushY);
    }
}
