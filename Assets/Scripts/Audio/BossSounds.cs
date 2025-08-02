using System.Collections.Generic;
using UnityEngine;

public class BossSounds : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _bossSound;
    [SerializeField]
    private AudioSource _myAudioSource;
    
    //Plays the charging  sound for the boss attack
    public void ReadyAttack()
    {
        _myAudioSource.PlayOneShot(_bossSound[0]);
    }
    //Plays the attack sound for the boss
    public void Shoot()
    {
        _myAudioSource.PlayOneShot(_bossSound[1]);
    }
}
