using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField]
    public GrapplePoint currentGrapplePoint;

    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    public bool hasGrapple = false;
    [SerializeField]
    private float pullBoostAmount = 2000;
    [SerializeField]
    private LineRenderer grappleLine;

    private bool _canPull;

    private IEnumerator _myCoroutine;

    private Vector3 lineStartLoc;

    [SerializeField]
    private float _circleCastRadius;

    [SerializeField]
    private LayerMask _grappleLM;

    [SerializeField]
    private SpriteRenderer _mySprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _canPull = true;
        _myCoroutine = ResetCanPull();
    }

    IEnumerator ResetCanPull()
    {
        yield return new WaitForSecondsRealtime(1f);
        _canPull = true;
        _mySprite.enabled = false;
        _myCoroutine = ResetCanPull();
    }


    //Detect if near grapple point and return reference
    #nullable enable
    public GrapplePoint? GetHook()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, _circleCastRadius, Vector2.zero, 0, _grappleLM);
        if (hit)
        {
            currentGrapplePoint = hit.collider.gameObject.GetComponent<GrapplePoint>();
            //Only allow the player to attatch if they are below the hook
            if (currentGrapplePoint.transform.position.y > gameObject.transform.position.y)
            {
                _mySprite.enabled = true;
                return currentGrapplePoint;
            }
            return null;
            
        }
        return null;
    }
    #nullable disable
    
    public void Pull()
    {
        //Calculate direction of grapple point
        //Apply force in that direction to player RB
        if (_canPull)
        {
            _mySprite.enabled = true;
            Vector2 grapplePointLoc = currentGrapplePoint.gameObject.transform.position;
            Vector2 playerLoc = myRigidBody.transform.position;
            Vector2 directionToMove = grapplePointLoc - playerLoc;
            _canPull = false;
            StartCoroutine(_myCoroutine);
            myRigidBody.AddForce(directionToMove.normalized * pullBoostAmount, ForceMode2D.Impulse);
        }

    }

    public void DrawGrappleLine()
    {
        grappleLine.SetPosition(0, currentGrapplePoint.transform.position);
        grappleLine.SetPosition(1, myRigidBody.transform.position);
    }

    public void RemoveGrappleLine()
    {
        currentGrapplePoint.Detatch();
        _mySprite.enabled = false;
        //Both set to the same position so it isn't visible on the screen
        grappleLine.SetPosition(0, lineStartLoc);
        grappleLine.SetPosition(1, lineStartLoc);
    }
}
