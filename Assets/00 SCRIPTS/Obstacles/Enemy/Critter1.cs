using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class Critter1 : ObstacleBase, IDamageable
{   
    private SpriteRenderer spriteRenderer;
    private Quaternion targetRotation;
    private Vector3 targetPosition;
    private float moveSpeed;
    private float moveTimer;
    private float moveInterval;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject zappedEffect;
    [SerializeField] private GameObject burnedEffect;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        moveSpeed = Random.Range(0.5f, 3f);

        GenerateRandomTargetPosition();
        GenerateMoveInterval();
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (moveTimer > 0)
            moveTimer -= Time.deltaTime;
        else
        {
            GenerateRandomTargetPosition();
            GenerateMoveInterval();
        }

        targetPosition -= new Vector3(GameManager.Instant.worldSpeed * Time.deltaTime, 0);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        //Hưởng vật đến target
        Vector3 relativePos = targetPosition - transform.position;
        if (relativePos != Vector3.zero)
        {
            //LookRotation có 2 tham số forward và upward đại diện cho trục Z và Y của vật luôn
            //Ở đây ta đang làm game 2D nên trục Z của vật sẽ hướng theo trục Z world (Vector3.forward) còn chúng ta chỉ thay
            //đổi trục Y của vật để mô tả việc vật hướng nhìn về đâu mà thôi.
            targetRotation = Quaternion.LookRotation(Vector3.forward, relativePos);
            //Ta có hàm RotateTowards nhằm xoay vật từ hướng hiện tại sang hướng chỉ định với tốc độ quay 1080 độ trên giây
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1080 * Time.deltaTime);
        }
        //Di chuyển theo BoostSpeed nữa
        Movement();
    }

    private void GenerateMoveInterval()
    {
        moveInterval = Random.Range(0.3f, 2f);
        moveTimer = moveInterval;
    }

    private void GenerateRandomTargetPosition()
    {
        float randomX = Random.Range(-5f, 5f);
        float randomY = Random.Range(-5f, 5f);
        targetPosition = new Vector2(randomX, randomY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CONSTANT.TAG_PLAYER))
        {
            TakeDamage(0, CONSTANT.TAG_PLAYER);
        }
    }

    public void TakeDamage(int damageAmount, string damageSourceTag)
    {
        if (damageSourceTag == CONSTANT.TAG_BULLET)
        {
            GameObject zapEffect = ObjectPooling.Instant.GetObject(zappedEffect, transform.parent);
            zapEffect.transform.position = transform.position;
            zapEffect.transform.rotation = transform.rotation;
            zapEffect.SetActive(true);

            AudioManager.Instant.PlayDestroyCritterSound();
        }
        else if (damageSourceTag == CONSTANT.TAG_PLAYER)
        {
            GameObject burnEffect = ObjectPooling.Instant.GetObject(burnedEffect, transform.parent);
            burnEffect.transform.position = transform.position;
            burnEffect.transform.rotation = transform.rotation;
            burnEffect.SetActive(true);

            AudioManager.Instant.PlayBurnedCritterSound();
        }

        gameObject.SetActive(false);
        LevelManager.Instant.critterCounter++;
    }
}
