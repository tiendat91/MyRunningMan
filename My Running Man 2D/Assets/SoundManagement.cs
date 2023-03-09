using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{

    public AudioClip fireSound;
    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {

        audioSource = GameObject.Find("SoundManagement").GetComponent<AudioSource>();
        ;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlaySound()
    {
        audioSource.PlayOneShot(fireSound);
    }
}
