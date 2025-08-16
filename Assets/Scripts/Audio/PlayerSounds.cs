using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    [SerializeField]
    private List<AudioClip> _playerSound;
    [SerializeField]
    private AudioSource _myAudioSource;

    //Plays the sound for the first part of the players step
    public void StepOne()
    {
        _myAudioSource.PlayOneShot(_playerSound[4]);
    }

    //Plays the sound for the second part of the players step
    public void StepTwo()
    {
        _myAudioSource.PlayOneShot(_playerSound[5]);
    }

    //Plays the sound for the players slide
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

    //Plays the sound for the players wall slide
    public void WallSlideStart()
    {
        _myAudioSource.PlayOneShot(_playerSound[7]);
    }

    //Stops the sound for the players wall slide
    public void WallSlideStop()
    {
        _myAudioSource.Stop();
    }

    //Plays the sound for the players death
    public void Death()
    {
        _myAudioSource.PlayOneShot(_playerSound[2]);
    }   
}
