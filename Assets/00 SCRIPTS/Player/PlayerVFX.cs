using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;

    private void Start()
    {
        Observer.AddListener("playerDeath",playDeathEffect);
    }

    private void OnDestroy()
    {
        Observer.RemoveListener("playerDeath", playDeathEffect);
    }

    void playDeathEffect(object[] datas)
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
    }
}
