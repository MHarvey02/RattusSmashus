using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _menuSound;
    [SerializeField]
    private AudioSource _myAudioSource;

    //Plays the sound when a menu option has been selected 
    public void Select()
    {
        _myAudioSource.PlayOneShot(_menuSound[0]);
    }

    //Plays the sound when a menu option has been changed
    public void ChangeOption()
    {
        _myAudioSource.PlayOneShot(_menuSound[1]);
    }

}
