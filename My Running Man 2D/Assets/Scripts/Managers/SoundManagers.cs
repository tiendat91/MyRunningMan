using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagers : Singleton<SoundManagers>
{
    [Header("Music")]
    [SerializeField] private AudioClip[] mainThemes;

    private AudioSource _audioSource;
    private ObjectPooler _pooler;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _pooler = GetComponent<ObjectPooler>();
        PlayMusic();
    }

    private void PlayMusic()
    {
        if(_audioSource == null)
        {
            return;
        }

        int randomTheme = Random.Range(0, mainThemes.Length);

        _audioSource.clip = mainThemes[0];
        _audioSource.Play();
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        //Get audiosource game object
        GameObject newAudio = _pooler.GetObjectFromPool(); 
        newAudio.SetActive(true);
        
        //Get audiosource from object
        AudioSource source = newAudio.GetComponent<AudioSource>();

        //Setup audiosource
        source.clip = clip;
        source.volume = volume; 
        source.Play();
    }
}
