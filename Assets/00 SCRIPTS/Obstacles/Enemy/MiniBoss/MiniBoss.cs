using UnityEngine;

public class MiniBoss : ObstacleBase, IDamageable
{
    private MiniBossAvatar avatar;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject destroyEffect;

    private int lives;
    private int maxLive = 10;
    private int damage = 5;
    private int expToGive = 5;
    private float timer;
    private float frequency;
    private float amplitude;
    private float centerY;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        avatar = GetComponentInChildren<MiniBossAvatar>();
    }

    private void OnEnable()
    {
        InitMiniBoss();
        timer = this.transform.position.y;
        frequency = Random.Range(0.3f, 1f);
        amplitude = Random.Range(0.8f, 1.5f);
        centerY = this.transform.position.y;
    }

    private void Start()
    {
        InitMiniBoss();
    }

    private void Update()
    {
        Movement();
    }

    private void InitMiniBoss()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        lives = maxLive;
    }

    protected override void Movement()
    {
        transform.position += new Vector3(Random.Range(-0.8f, -1.5f) * Time.deltaTime, 0f);

        timer -= Time.deltaTime;
        float sine = Mathf.Sin(timer * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, centerY + sine);

        base.Movement();
    }

    public void TakeDamage(int damageAmount, string damageSourceTag)
    {
        if (damageAmount <= 0) return;

        lives -= damageAmount;
        avatar.ChangeMaterialWhenHit();
        if (lives <= 0)
        {
            GameObject effect = ObjectPooling.Instant.GetObject(destroyEffect, transform.parent);
            effect.transform.position = transform.position;
            effect.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            effect.SetActive(true);

            AudioManager.Instant.PlayDestroyMiniBossSound();
            avatar.GetComponent<SpriteRenderer>().material = avatar.defaulMaterial;

            gameObject.SetActive(false);

            Observer.Notify("dropExp", expToGive);
        }
        else
        {
            AudioManager.Instant.PlayHitMiniBossSound();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageableTarget))
        {
            damageableTarget.TakeDamage(this.damage, this.gameObject.tag);
        }
    }
}
