using System.Collections.Generic;
using UnityEngine;

public class TrapSounds : MonoBehaviour
{

    [SerializeField]
    private List<AudioClip> _trapSound;
    [SerializeField]
    private AudioSource _myAudioSource;

    public void Trap()
    {
        _myAudioSource.PlayOneShot(_trapSound[0]);
    } 
}
