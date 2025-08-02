using System.Collections.Generic;
using UnityEngine;

public class ShotgunSound : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _shotgunSound;
    [SerializeField]
    private AudioSource _myAudioSource;

    //Plays the sound for the shotgun shooting
    public void Shoot()
    {
        _myAudioSource.PlayOneShot(_shotgunSound[0]);
    }

    //Plays the sound for the shotgun reloading
    public void Reload()
    {
        _myAudioSource.PlayOneShot(_shotgunSound[1]);
    }
}
