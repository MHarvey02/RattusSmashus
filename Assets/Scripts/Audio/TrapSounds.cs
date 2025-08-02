using System.Collections.Generic;
using UnityEngine;

public class TrapSounds : MonoBehaviour
{

    [SerializeField]
    private List<AudioClip> _trapSound;
    [SerializeField]
    private AudioSource _myAudioSource;
    //Plays the sound for the trap when it has been collided with
    public void Trap()
    {
        _myAudioSource.PlayOneShot(_trapSound[0]);
    } 
}
