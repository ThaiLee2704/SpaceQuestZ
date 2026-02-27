using System.Collections.Generic;
using UnityEngine;

public class MiniBoss2 : ObstacleBase, IDamageable
{
    private MiniBossAvatar avatar;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private List<Frames> frames;

    private int lives;
    private int maxLive = 10;
    private int damage = 2;
    private int expToGive = 2;
    private float speedX;
    private float speedY;
    private int enemyVariant;
    private bool isCharging;

    [System.Serializable]
    private class Frames
    {
        public Sprite[] sprites;
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        avatar = GetComponentInChildren<MiniBossAvatar>();
    }

    private void OnEnable()
    {
        InitMiniBoss();
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
        EnterIdle();
        enemyVariant = Random.Range(0, frames.Count);
        lives = maxLive;
    }

    private void EnterIdle()
    {
        isCharging = false;
        spriteRenderer.sprite = frames[enemyVariant].sprites[0];
        speedX = Random.Range(0.1f, 0.6f);
        speedY = Random.Range(-1f, 1f);
    }

    private void EnterCharge()
    {
        if (!isCharging)
        {
            isCharging = true;
            spriteRenderer.sprite = frames[enemyVariant].sprites[1];
            speedX = Random.Range(-4f, -6f);
            speedY = 0f;
            AudioManager.Instant.PlayChargeMiniBoss2Sound();
        }
    }

    protected override void Movement()
    {
        transform.position += new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime);

        if (transform.position.y > 5 || transform.position.y < -5)
        {
            speedY *= -1;
        }

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

            AudioManager.Instant.PlayDestroyMiniBoss2Sound();
            avatar.GetComponent<SpriteRenderer>().material = avatar.defaulMaterial;

            gameObject.SetActive(false);

            Observer.Notify("dropExp", expToGive);
        }
        else if (lives <= maxLive * 0.5f)
        {
            EnterCharge();
        }
        else
        {
            AudioManager.Instant.PlayHitMiniBoss2Sound();
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
