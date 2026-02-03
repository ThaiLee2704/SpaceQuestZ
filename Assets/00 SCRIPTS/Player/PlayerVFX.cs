using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;
    [SerializeField] ParticleSystem boostEffect;

    private void Start()
    {
        Observer.AddListener(CONSTANT.OBSERVER_PLAYERDEATH,PlayDeathEffect);
        Observer.AddListener(CONSTANT.OBSERVER_PLAYERBOOST, PlayerBoostEffect);
    }

    private void OnDestroy()
    {
        Observer.RemoveListener(CONSTANT.OBSERVER_PLAYERDEATH, PlayDeathEffect);
        Observer.RemoveListener(CONSTANT.OBSERVER_PLAYERBOOST, PlayerBoostEffect);
    }

    void PlayerBoostEffect(object[] datas)
    {
        boostEffect.Play();
    }

    void PlayDeathEffect(object[] datas)
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
    }
}
