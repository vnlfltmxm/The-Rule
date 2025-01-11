using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    public bool IsSoundEnabled = true;
    public float MusicVolume = 0.5f;
    public float SFXVolume = 0.5f;

    protected override void Init()
    {
        
    }
    public void SetSoundEnabled(bool isEnabled)
    {
        IsSoundEnabled = isEnabled;
    }
    public void SetMusicVolume(float volume)
    {
        MusicVolume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        SFXVolume = volume;
    }

    
}
