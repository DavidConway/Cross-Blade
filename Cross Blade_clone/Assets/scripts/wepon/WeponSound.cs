using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeponSound : MonoBehaviour
{
    AudioSource audioPlayer;

    [SerializeField]
    private AudioClip[] hitSounds;

    [SerializeField]
    private AudioClip chargeSound;

    [SerializeField]
    private AudioClip[] blockSounds;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    public void PlayHit()
    {
        int rn = Random.Range(0, hitSounds.Length - 1);
        audioPlayer.PlayOneShot(hitSounds[rn]);
    }

    public void PlayBlock()
    {
        int rn = Random.Range(0, blockSounds.Length - 1);
        audioPlayer.PlayOneShot(blockSounds[rn]);
        audioPlayer.Play();
    }

    public void PlayCharge()
    {
        audioPlayer.PlayOneShot(chargeSound);
    }

}
