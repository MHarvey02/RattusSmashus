using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    [SerializeField]
    private List<AudioClip> _playerSound;
    [SerializeField]
    private AudioSource _myAudioSource;

    public void StepOne()
    {
        _myAudioSource.PlayOneShot(_playerSound[4]);
    }

    public void StepTwo()
    {
        _myAudioSource.PlayOneShot(_playerSound[5]);
    }

    public void Slide()
    {
        _myAudioSource.PlayOneShot(_playerSound[6]);
    }

    //Plays the sound for the players jump
    public void Jump()
    {
        _myAudioSource.PlayOneShot(_playerSound[0]);
    }

    //Plays the sound for the players double jump
    public void DoubleJump()
    {
        _myAudioSource.PlayOneShot(_playerSound[1]);
    }

    //Plays the sound for the players grapple
    public void Grapple()
    {
        _myAudioSource.PlayOneShot(_playerSound[3]);
    }

    //Plays the sound for the players death
    public void Death()
    {
        _myAudioSource.PlayOneShot(_playerSound[2]);
    }

    
}
