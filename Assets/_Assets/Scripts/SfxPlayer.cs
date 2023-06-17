using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip[] clips;

    AudioSource audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();    
    }

    public void PositiveSfx()
    {
        audioPlayer.clip = clips[0];
        audioPlayer.Play();
    }

    public void NegativeSfx()
    {
        audioPlayer.clip = clips[1];
        audioPlayer.Play();
    }

    public void CountdownSfx()
    {
        audioPlayer.clip = clips[2];
        audioPlayer.Play();
    }
}
