
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
    bool hasShotgun = false;

    [SerializeField]
    SpriteRenderer myRenderer;
    
    private IEnumerator coroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aimDirection = new Vector2(1, 0);
    }

    public void SetAimDirection(InputAction.CallbackContext inputContext)
    {
        if (inputContext.canceled)
        {
            return;
        }

        aimDirection = inputContext.ReadValue<Vector2>();

        Debug.Log(aimDirection.normalized);


    }

    public void Shoot()
    {
        if (canShoot && hasShotgun)
        {
            myRenderer.enabled = true;
            coroutine = Reload();
            //create bullets
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
        Debug.Log("can fire");

    }

}
