using UnityEngine;

public class Boss1 : ObstacleBase
{
    private float playerPositionX;

    [SerializeField] private GameObject destroyEffect;
    private Boss1Animation boss1Animation;

    [SerializeField] private int lives;

    private float speedX;
    private float speedY;
    private bool charging;
    public bool Charging => charging;

    private float switchInterval;
    private float switchTimer;

    private void OnEnable()
    {
        boss1Animation = GetComponentInChildren<Boss1Animation>();
        lives = 30;
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
            if (charging && transform.position.x > playerPositionX)
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
        if (GameManager.Instant.Player.IsPlayerBoosting && !charging)
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

        charging = false;
    }

    void EnterChargeState()
    {
        if (!charging)
            AudioManager.Instant.PlayBossChargeSound();
        speedX = -10f;
        speedY = 0;

        switchInterval = Random.Range(0.6f, 1.3f); ;
        switchTimer = switchInterval;

        charging = true;

    }

    public void TakeDamage(int damage)
    {
        AudioManager.Instant.PlayHitBossSound();
        lives -= damage;
        boss1Animation.OnChangeMaterialWhenHit();
        if (lives <= 0)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity); //Sửa lại DestroyEffect khi boss bị diệt
            //AudioManager.Instant.PlayDestroyAsteroidSound();
            boss1Animation.GetComponent<SpriteRenderer>().material = boss1Animation.defaulMaterial;
            lives = 30;
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CONSTANT.TAG_BULLET))
        {
            TakeDamage(1);
        }
    }
}
