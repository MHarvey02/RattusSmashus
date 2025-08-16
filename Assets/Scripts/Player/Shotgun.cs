
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.Mathematics;

public class Shotgun : MonoBehaviour
{

    [SerializeField]
    private Vector2 aimDirection;

    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private float kickbackMultiplier = 1000;

    [SerializeField]
    public bool canShoot = true;

    [SerializeField]
    float ReloadTime = 3;

    [SerializeField]
    private int _shotAmount = 5;

    [SerializeField]
    public bool hasShotgun = false;

    [SerializeField]
    SpriteRenderer myRenderer;

    [SerializeField]
    UnityEvent shotFiredEvent;

    [SerializeField]
    private HUD _myHud;
    [SerializeField]
    private Projectile _projectile;

    [SerializeField]
    private ShotgunSound _mySounds;
    
    private IEnumerator coroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRenderer.enabled = false;
        aimDirection = new Vector2(0, 0);

        shotFiredEvent.AddListener(_myHud.LowerShotgunAlpha);
    }

    public void SetAimDirection(InputAction.CallbackContext inputContext)
    {
        aimDirection = inputContext.ReadValue<Vector2>();
    }

    public void Shoot(InputAction.CallbackContext inputContext)
    {

        if (inputContext.started)
        {
            //Check if the player has the shotgun, can shoot and is aiming
            if (canShoot && hasShotgun && aimDirection != new Vector2(0, 0))
            {
                myRenderer.enabled = true;
                _mySounds.Shoot();
                coroutine = Reload();
                for (int i = 0; i < _shotAmount; i++)
                {
                    Projectile bullet = PlayerObjectPool.SharedInstance.GetPooledObject();
                    if (bullet != null)
                    {
                        bullet.gameObject.SetActive(true);
                        bullet.gameObject.transform.rotation = new quaternion(aimDirection.x, aimDirection.y, 0, 0);
                        bullet.PlayerShoot(aimDirection);
                        bullet.transform.position = transform.position;
                    }
                }
                canShoot = false;
                KickBack();
                shotFiredEvent.Invoke();
                StartCoroutine(coroutine);
            }
        return;
        }
        
    }
        
    //Apply backwards force to the player as they shoot to simulate kickback
    private void KickBack()
    {
        myRigidBody.linearVelocity = new Vector2(0, 0);
        Vector2 kickbackDirection = -aimDirection * kickbackMultiplier;
        Debug.Log(kickbackDirection);
        myRigidBody.AddForce(kickbackDirection);
    }
    //Delay between shots
    private IEnumerator Reload()
    {
        yield return new WaitForSecondsRealtime(ReloadTime);
        canShoot = true;
        _mySounds.Reload();
        myRenderer.enabled = false;

    }

}
    
