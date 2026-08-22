using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource mainAudioSource;

    public AudioSource secondaryAudioSource;

    //SOUND EFFECTS
    public AudioClip keyboardClicking;

    //BACKGROUND NOISES
    public AudioClip whiteNoise;

    public float volume = 1;

    void Update()
    {
        mainAudioSource.volume = volume;
        secondaryAudioSource.volume = volume;
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
