using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource mainAudioSource;

    public AudioSource secondaryAudioSource;

    //SOUND EFFECTS
    public AudioClip keyboardClicking;
    public AudioClip gettingOutMatch;
    public AudioClip lightingMatch;
    public AudioClip whoosingOfFlame;
    public AudioClip itemPickup;
    public AudioClip safeClicking;
    public AudioClip safeUnlock;
    public AudioClip tapingWood;
    public AudioClip thud;
    public AudioClip meow;



    //BACKGROUND NOISES
    public AudioClip whiteNoise;
    public AudioClip lowIntenseFire;
    public AudioClip backgroundMusic;

    public float musicVolume = 0.5f;
    public float sfxVolume = 0.5f;

    void Update()
    {
        mainAudioSource.volume = sfxVolume;
        secondaryAudioSource.volume = musicVolume;
    }
    public void PlaySoundEffect(AudioClip audioClip)
    { 
        mainAudioSource.clip = audioClip;
        mainAudioSource.Play();
    }

    public void PlayBackgroundNoise(AudioClip backgroundNoise)
    {
        secondaryAudioSource.clip = backgroundNoise;
        secondaryAudioSource.Play();
    }

    public void StopBackgroundMusic()
    {
        secondaryAudioSource.Stop();
    }
}
