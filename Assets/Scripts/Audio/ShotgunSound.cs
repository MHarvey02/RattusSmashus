using System.Collections.Generic;
using UnityEngine;

public class ShotgunSound : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _shotgunSound;
    [SerializeField]
    private AudioSource _myAudioSource;

    public void Shoot()
    {
        _myAudioSource.PlayOneShot(_shotgunSound[0]);
    }

    public void Reload()
    {
        _myAudioSource.PlayOneShot(_shotgunSound[1]);
    }
}
