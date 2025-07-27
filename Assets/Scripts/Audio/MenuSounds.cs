using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _menuSound;
    [SerializeField]
    private AudioSource _myAudioSource;
    public void Select()
    {
        _myAudioSource.PlayOneShot(_menuSound[0]);
    }

    public void ChangeOption()
    {
        _myAudioSource.PlayOneShot(_menuSound[1]);
    }

}
