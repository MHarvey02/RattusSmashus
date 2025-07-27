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

    public void DoubleJump()
    {
        _myAudioSource.PlayOneShot(_playerSound[1]);
    }

    public void Grapple()
    {
         _myAudioSource.PlayOneShot(_playerSound[3]);
    }

    public void Death()
    {
        _myAudioSource.PlayOneShot(_playerSound[2]);
    }
    
}
