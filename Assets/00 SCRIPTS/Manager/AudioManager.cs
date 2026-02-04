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
