using UnityEngine;

public class PhaserWeapon : Singleton<PhaserWeapon>
{
    [SerializeField] private GameObject prefabs;

    public float speed;
    public int damage;

    [SerializeField] private float attackSpeed = 0.3f;
    [SerializeField] private float nextFireTime = 0;

    private Transform bulletContainer;

    private void Start()
    {
        GameObject go = new GameObject("-----BULLET CONTAINER-----");
        bulletContainer = go.transform;
    }

    public void Shoot()
    {
        if (Time.time >= nextFireTime)  //Thời gian hiện tại lớn hơn thời gian cho lần bắn tiếp theo thì cho bắn
        {
            nextFireTime = Time.time + attackSpeed; //Bắn 1 cái thì tính được tg cho lần bắn tiếp theo bằng cách lấy tg hiện tại + attackSpeed

            GameObject bullet = ObjectPooling.Instant.GetObject(prefabs, bulletContainer);
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.SetActive(true);
            }
        }
    }
}
