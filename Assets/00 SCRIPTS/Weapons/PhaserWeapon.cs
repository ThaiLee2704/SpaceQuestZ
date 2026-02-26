using UnityEngine;

public class PhaserWeapon : Weapon
{
    private GameObject go;
    private Transform bulletContainer;

    [Header("Global Info Bullet")]
    [SerializeField] private GameObject prefabs;
    [SerializeField] private float attackSpeed = 0.3f;
    [SerializeField] private float nextFireTime = 0;

    private void Start()
    {
        go = new GameObject("-----BULLET CONTAINER-----");
        bulletContainer = go.transform;
    }

    private void OnDestroy()
    {
        if (go != null) Destroy(go);
    }

    public override void FireBullet()
    {
        base.FireBullet();

        if (Time.time >= nextFireTime)  //Thời gian hiện tại lớn hơn thời gian cho lần bắn tiếp theo thì cho bắn
        {
            nextFireTime = Time.time + attackSpeed; //Bắn 1 cái thì tính được tg cho lần bắn tiếp theo bằng cách lấy tg hiện tại + attackSpeed

            AudioManager.Instant.PlayFireBulletSound();

            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                GameObject bulletObj = ObjectPooling.Instant.GetObject(prefabs, bulletContainer);

                if (bulletObj != null)
                {
                    float yPos = transform.position.y;

                    if (stats[weaponLevel].amount > 1)
                    {
                        float spacing = stats[weaponLevel].range / (stats[weaponLevel].amount - 1);
                        yPos = transform.position.y - (stats[weaponLevel].range / 2) + i * spacing;
                    }

                    bulletObj.transform.position = new Vector2(transform.position.x, yPos);
                    bulletObj.transform.localScale = new Vector2(stats[weaponLevel].size, stats[weaponLevel].size);
                    bulletObj.SetActive(true);

                    if (bulletObj.TryGetComponent(out PhaserBullet bullet))
                    {
                        bullet.Setup(stats[weaponLevel].damage, stats[weaponLevel].speed);
                    }
                }
            }
        }
    }
}
