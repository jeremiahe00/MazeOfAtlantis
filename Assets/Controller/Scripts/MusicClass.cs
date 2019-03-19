using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicClass : MonoBehaviour
{
    private AudioSource _audioSource;
    private float musicVolume = 1f;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
        //_audioSource = Resources.Load("View/ViewMainMenu/Music") as AudioSource;
        
    }

    public void Update()
    {
        _audioSource.volume = musicVolume;
        //this.gameObject.GetComponent<AudioSource>().volume = musicVolume;

    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}
