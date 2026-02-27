using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private IEnumerator Start()
    {
        //Chờ đến cuối frame đầu tiên (hoặc chờ 1 frame)
        //Để AudioMixer khởi tạo xong 100%
        yield return new WaitForEndOfFrame();

        float savedMusic = PlayerPrefs.GetFloat("musicVolume", 1f);
        float savedSFX = PlayerPrefs.GetFloat("sfxVolume", 1f);

        musicSlider.value = savedMusic;
        SFXSlider.value = savedSFX;

        ApplyVolume("music", savedMusic);
        ApplyVolume("sfx", savedSFX);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        ApplyVolume("music", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        ApplyVolume("sfx", volume); 
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void ApplyVolume(string paramName, float volume)
    {
        if (volume <= 0.0001f) volume = 0.0001f;

        audioMixer.SetFloat(paramName, Mathf.Log10(volume) * 20);
    }
}
