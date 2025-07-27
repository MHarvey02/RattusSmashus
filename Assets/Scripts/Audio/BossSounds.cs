using System.Collections.Generic;
using UnityEngine;

public class BossSounds : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _bossSound;
    [SerializeField]
    private AudioSource _myAudioSource;
    public void ReadyAttack()
    {
        _myAudioSource.PlayOneShot(_bossSound[0]);
    }

    public void Shoot()
    {
        _myAudioSource.PlayOneShot(_bossSound[1]);
    }
}
