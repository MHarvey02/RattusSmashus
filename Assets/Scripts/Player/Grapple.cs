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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _canPull = true;

        // Set the material
        //lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Set the color
        grappleLine.startColor = Color.blue;
        grappleLine.endColor = Color.blue;

        // Set the width
        grappleLine.startWidth = 0.2f;
        grappleLine.endWidth = 0.2f;

        // Set the number of vertices
        grappleLine.positionCount = 2;

        _myCoroutine = ResetCanPull();

    }

    IEnumerator ResetCanPull()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        _canPull = true;
        _myCoroutine = ResetCanPull();
    }


    //Detect if near grapple point and return ref
    #nullable enable
    public GrapplePoint? GetHook()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, _circleCastRadius, Vector2.zero, 0, _grappleLM);
        if (hit)
        {
            currentGrapplePoint = hit.collider.gameObject.GetComponent<GrapplePoint>();
            return currentGrapplePoint;
        }
        return null;
    }
    #nullable disable
    
    public void Pull()
    {
        //calculate direction of grapple point
        //apply force in that direction to player RB
        if (_canPull)
        {
            DrawGrappleLine();
            Vector2 grapplePointLoc = currentGrapplePoint.gameObject.transform.position;
            Vector2 playerLoc = myRigidBody.transform.position;
            Vector2 directionToMove = grapplePointLoc - playerLoc;
            _canPull = false;
            StartCoroutine(_myCoroutine);
            myRigidBody.AddForce(directionToMove.normalized * pullBoostAmount, ForceMode2D.Impulse);
            RemoveGrappleLine();
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
        grappleLine.SetPosition(0, lineStartLoc);
        grappleLine.SetPosition(1, lineStartLoc);
    }



}
