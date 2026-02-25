using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;
    [SerializeField] ParticleSystem boostEffect;

    private void OnEnable()
    {
        Observer.AddListener(CONSTANT.OBSERVER_PLAYERDEATH,PlayerDeathEffect);
        Observer.AddListener(CONSTANT.OBSERVER_PLAYERBOOST, PlayerBoostEffect);
    }

    private void OnDisable()
    {
        Observer.RemoveListener(CONSTANT.OBSERVER_PLAYERDEATH, PlayerDeathEffect);
        Observer.RemoveListener(CONSTANT.OBSERVER_PLAYERBOOST, PlayerBoostEffect);
    }

    void PlayerBoostEffect(object[] datas)
    {
        boostEffect.Play();
    }

    void PlayerDeathEffect(object[] datas)
    {
        GameObject effect = ObjectPooling.Instant.GetObject(deathEffect, transform.parent); //Sửa lại DestroyEffect khi boss bị diệt
        effect.transform.position = transform.position;
        effect.transform.rotation = transform.rotation;
        effect.SetActive(true);
    }
}
