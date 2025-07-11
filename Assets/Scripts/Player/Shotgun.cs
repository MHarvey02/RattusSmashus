
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Shotgun : MonoBehaviour
{

    [SerializeField]
    private Vector2 aimDirection;

    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private float kickbackMultiplier = 1000000;

    [SerializeField]
    bool canShoot = true;

    [SerializeField]
    float ReloadTime = 3;

    [SerializeField]
    private int _shotAmount = 5;

    [SerializeField]
    bool hasShotgun = false;

    [SerializeField]
    SpriteRenderer myRenderer;
    
    private IEnumerator coroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRenderer.enabled = false;
        aimDirection = new Vector2(0, 0);
    }

    public void SetAimDirection(InputAction.CallbackContext inputContext)
    {
        aimDirection = inputContext.ReadValue<Vector2>();
    }

    public void Shoot()
    {

        
        if (canShoot && hasShotgun && aimDirection != new Vector2(0,0))
        {
            myRenderer.enabled = true;
            coroutine = Reload();
            for (int i = 0; i < _shotAmount; i++)
            {
                Projectile bullet = ObjectPool.SharedInstance.GetPooledObject();
                if (bullet != null)
                {
                    bullet.SetLocation(aimDirection * 10);
                    bullet.transform.position = transform.position;
                    //bullet.transform.rotation = transform.rotation;
                    bullet.gameObject.SetActive(true);
                }
            }



            canShoot = false;
            KickBack();
            StartCoroutine(coroutine);
        }
        return;
    }
        

    private void KickBack()
    {
        myRigidBody.linearVelocity = new Vector2(0, 0);
        Vector2 kickbackDirection = -aimDirection * kickbackMultiplier;
        
        myRigidBody.AddForce(kickbackDirection);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSecondsRealtime(ReloadTime);
        canShoot = true;
        myRenderer.enabled = false;
        Debug.Log("can fire");

    }

    }
    
