using UnityEngine;

public class Boss1 : ObstacleBase, IDamageable
{
    private Boss1Animation boss1Animation;
    private float playerPositionX;
    private float speedX;
    private float speedY;
    private bool isCharging;
    private float switchInterval;
    private float switchTimer;
    private int damage = 20;
    private int lives;
    private int maxLives = 100;
    private int expToGive = 20;

    [SerializeField] private GameObject destroyEffect;
    public bool IsCharging => isCharging;

    private void Awake()
    {
        boss1Animation = GetComponentInChildren<Boss1Animation>();
    }

    private void OnEnable()
    {
        lives = maxLives;
        EnterChargeState();
        AudioManager.Instant.PlayBossSpawnSound();
    }

    void Update()
    {
        playerPositionX = GameManager.Instant.Player.transform.position.x;

        if (switchTimer > 0)
            switchTimer -= Time.deltaTime;
        else
        {
            if (isCharging && transform.position.x > playerPositionX)
                EnterPatrolState();
            else
                EnterChargeState();
        }

        Movement();
    }

    protected override void Movement()
    {
        if (transform.position.y > 3 || transform.position.y < -3)
            speedY *= -1;
        else if (transform.position.x < playerPositionX)
            EnterChargeState();

        float moveX;
        if (GameManager.Instant.Player.IsPlayerBoosting && !isCharging)
            moveX = GameManager.Instant.worldSpeed * Time.deltaTime * -1f;
        else
            moveX = speedX * Time.deltaTime;
        float moveY = speedY * Time.deltaTime;
        transform.position += new Vector3(moveX, moveY);

        if (transform.position.x < -12f)
            gameObject.SetActive(false);
    }

    void EnterPatrolState()
    {
        speedX = 0f;
        speedY = Random.Range(-2f, 2f);

        switchInterval = Random.Range(5f, 10f);
        switchTimer = switchInterval;

        isCharging = false;
    }

    void EnterChargeState()
    {
        if (!isCharging)
            AudioManager.Instant.PlayBossChargeSound();
        speedX = -10f;
        speedY = 0;

        switchInterval = Random.Range(0.6f, 1.3f); ;
        switchTimer = switchInterval;

        isCharging = true;

    }

    public void TakeDamage(int damageAmount, string damageSourceTag)
    {
        if (damageAmount <= 0) return;

        AudioManager.Instant.PlayHitBossSound();
        lives -= damageAmount;
        boss1Animation.ChangeMaterialWhenHit();
        if (lives <= 0)
        {
            GameObject effect = ObjectPooling.Instant.GetObject(destroyEffect, transform.parent); 
            effect.transform.position = transform.position;
            effect.transform.rotation = Quaternion.identity;
            effect.SetActive(true);

            AudioManager.Instant.PlayDestroyAsteroidSound();
            boss1Animation.GetComponent<SpriteRenderer>().material = boss1Animation.defaulMaterial;
            gameObject.SetActive(false);
            Observer.Notify("dropExp", expToGive);
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
