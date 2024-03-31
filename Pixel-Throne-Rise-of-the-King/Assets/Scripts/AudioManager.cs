using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //audio source
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    //audio clip
    public AudioClip background;
    public AudioClip death;
    public AudioClip hit;

    private void Start() 
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
