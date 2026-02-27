using System.Collections;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Effect Game")]
    [SerializeField] private AudioSource death;
    [SerializeField] private AudioSource boost;
    [SerializeField] private AudioSource hitPlayer;
    [SerializeField] private AudioSource destroyAsteroid;
    [SerializeField] private AudioSource hitRock;
    [SerializeField] private AudioSource fireBullet;
    [SerializeField] private AudioSource destroyCritter;
    [SerializeField] private AudioSource burnedCritter;
    [SerializeField] private AudioSource hitBoss;
    [SerializeField] private AudioSource bossCharge;
    [SerializeField] private AudioSource bossSpawn;
    [SerializeField] private AudioSource hitMiniBoss;
    [SerializeField] private AudioSource destroyMiniBoss;
    [SerializeField] private AudioSource chargeMiniBoss2;
    [SerializeField] private AudioSource hitMiniBoss2;
    [SerializeField] private AudioSource destroyMiniBoss2;
    [Header("Effect UI")]
    [SerializeField] private AudioSource pause;
    [SerializeField] private AudioSource unpause;
    [Header("Music")]
    [SerializeField] private AudioSource mainMenuSound;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource gameWinSound;

    private void Start()
    {
        Observer.AddListener(CONSTANT.OBSERVER_PLAYERDEATH, PlayDeathSound);
        Observer.AddListener(CONSTANT.OBSERVER_PLAYERHIT, PlayHitPlayerSound);
        Observer.AddListener(CONSTANT.OBSERVER_PLAYERBOOST, PlayBoostSound);
    }

    private void OnDestroy()
    {
        Observer.RemoveListener(CONSTANT.OBSERVER_PLAYERDEATH, PlayDeathSound);
        Observer.RemoveListener(CONSTANT.OBSERVER_PLAYERHIT, PlayHitPlayerSound);
        Observer.RemoveListener(CONSTANT.OBSERVER_PLAYERBOOST, PlayBoostSound);
    }
    public void PlayHitMiniBoss2Sound()
    {
        PlaySource(hitMiniBoss2);
    }
    public void PlayDestroyMiniBoss2Sound()
    {
        PlaySource(destroyMiniBoss2);
    }
    public void PlayChargeMiniBoss2Sound()
    {
        PlaySource(chargeMiniBoss2);
    }
    public void PlayHitMiniBossSound()
    {
        PlaySource(hitMiniBoss);
    }
    public void PlayDestroyMiniBossSound()
    {
        PlaySource(destroyMiniBoss);
    }
    public void PlayBossSpawnSound()
    {
        PlaySource(bossSpawn);
    }
    public void PlayBossChargeSound()
    {
        fireBullet.pitch = Random.Range(0.7f, 1.3f);
        PlaySource(bossCharge);
    }
    public void PlayHitBossSound()
    {
        PlaySource(hitBoss);
    }
    public void PlayBurnedCritterSound()
    {
        fireBullet.pitch = Random.Range(0.7f, 1.3f);
        PlaySource(burnedCritter);
    }
    public void PlayDestroyCritterSound()
    {
        fireBullet.pitch = Random.Range(0.7f, 1.3f);
        PlaySource(destroyCritter);
    }
    public void PlayFireBulletSound()
    {
        fireBullet.pitch = Random.Range(0.7f, 1.3f);
        PlaySource(fireBullet);
    }
    public void PlayHitRockSound()
    {
        PlaySource(hitRock);
    }
    public void PlayDestroyAsteroidSound()
    {
        PlaySource(destroyAsteroid);
    }
    private void PlayBoostSound(object[] datas)
    {
        PlaySource(boost);
    }
    public void PlayPauseSound()
    {
        PlaySource(pause);
    }
    public void PlayUnpauseSound()
    {
        PlaySource(unpause);
    }
    private void PlayDeathSound(object[] datas)
    {
        PlaySource(death);
    }
    private void PlayHitPlayerSound(object[] datas)
    {
        PlaySource(hitPlayer);
    }
    public void PlayMainMenuSound()
    {
        PlaySource(mainMenuSound);
    }
    public void StopMainMenuSound()
    {
        mainMenuSound.Stop();
    }
    public void PlayGameOverSound()
    {
        PlaySource(gameOverSound);
    }
    public void PlayGameWinSound()
    {
        PlaySource(gameWinSound);
    }

    private void PlaySource(AudioSource source)
    {
        if (source != null)
        {
            source.Stop();
            source.Play();
        }
    }
}
