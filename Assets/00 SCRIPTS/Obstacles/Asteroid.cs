using System.Collections;
using UnityEngine;

public class Asteroid : ObstacleBase
{
    private SpriteRenderer spriteRenderer;

    private Material defaulMaterial;
    [SerializeField] Material hitMaterial;

    private Rigidbody2D rb;

    [SerializeField] private Sprite[] sprites;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CONSTANT.TAG_PLAYER) || collision.gameObject.CompareTag(CONSTANT.TAG_BULLET))
        {
            spriteRenderer.material = hitMaterial;
            StartCoroutine(ResetMaterial());
        }
    }
    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaulMaterial;
    }
}
