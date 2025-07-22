using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    [SerializeField]
    private List<AudioClip> _playerSound;
    [SerializeField]
    private AudioSource _myAudioSource;

    public void Jump()
    {
        _myAudioSource.PlayOneShot(_playerSound[0]);
    }
    public void Shoot()
    {
        _myAudioSource.PlayOneShot(_playerSound[1]);
    }
}
