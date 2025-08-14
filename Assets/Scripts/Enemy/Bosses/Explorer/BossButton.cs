using UnityEngine;
using UnityEngine.Events;
public class BossButton : MonoBehaviour
{

    [SerializeField]
    private bool _hasBeenActivated = false;

    [SerializeField]
    private UnityEvent _damageBossEvent;
    [SerializeField]
    private Animator _myAnimator;

    [SerializeField]
    private AudioClip _mySound;
    [SerializeField]
    private AudioSource _myAudioSource;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && _hasBeenActivated == false)
        {
            _hasBeenActivated = true;
            _damageBossEvent.Invoke();
            _myAnimator.Play("Activated");
            _myAudioSource.PlayOneShot(_mySound);
        }
    }


}
