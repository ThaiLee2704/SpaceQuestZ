using UnityEngine;

public class PhaserWeapon : Singleton<PhaserWeapon>
{
    [SerializeField] private GameObject prefabs;

    public float speed;
    public int damage;

    private float timeCount = 0.3f;
    private float timer = 0;

    public void Shoot()
    {
        timer += Time.deltaTime;
        if (timer > timeCount)
        {
            Instantiate(prefabs, transform.position, Quaternion.identity);
            timer = 0;
        }    
    }
}
