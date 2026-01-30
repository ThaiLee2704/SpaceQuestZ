using System.Collections;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource death;
    [SerializeField] private AudioSource boost;
    [SerializeField] private AudioSource hit;
    [SerializeField] private AudioSource pause;
    [SerializeField] private AudioSource unpause;
    [SerializeField] private AudioSource mainMenuSound;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource gameWinSound;

    private void Start()
    {
        Observer.AddListener(CONSTANT.OBSERVER_PLAYERDEATH, PlayDeathSound);
        Observer.AddListener(CONSTANT.OBSERVER_PLAYERHIT, PlayDeathHit);
    }

    private void OnDestroy()
    {
        Observer.RemoveListener(CONSTANT.OBSERVER_PLAYERDEATH, PlayDeathSound);
        Observer.RemoveListener(CONSTANT.OBSERVER_PLAYERHIT, PlayDeathHit);
    }
    public void PlayBoostSound()
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
    private void PlayDeathHit(object[] datas)
    {
        PlaySource(hit);
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
