using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicMenu : MonoBehaviour
{

    public Slider _musicSlider, _sfxSlider;
    public Text musicLabel,sfxLabel;
    private void Start()
    {
        _musicSlider.value = AudioManager.instance.musicSource.volume;
        _sfxSlider.value = AudioManager.instance.sfxSource.volume;
    }
    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();

        if (AudioManager.instance.musicSource.mute)
        {
            musicLabel.GetComponent<ButtonSelect>().currentColor = Color.gray;
        }
        else
        {
            musicLabel.GetComponent <ButtonSelect>().currentColor = Color.white;
        }

        
    }

    public void ToggleSFX()
    {
        AudioManager.instance.ToggleSFX();
        if (AudioManager.instance.sfxSource.mute)
        {
            sfxLabel.GetComponent<ButtonSelect>().currentColor = Color.gray;
        }
        else
        {
            sfxLabel.GetComponent<ButtonSelect>().currentColor = Color.white;
        }
        
    }

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.instance.SFXVolume(_sfxSlider.value);
    }
}
