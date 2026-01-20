using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;

    private void Start()
    {
        Observer.AddListener(CONSTANT.OBSERVER_PLAYERDEATH,playDeathEffect);
    }

    private void OnDestroy()
    {
        Observer.RemoveListener(CONSTANT.OBSERVER_PLAYERDEATH, playDeathEffect);
    }

    void playDeathEffect(object[] datas)
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
    }
}
