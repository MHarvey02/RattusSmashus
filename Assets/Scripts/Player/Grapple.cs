using Unity.VisualScripting;

using UnityEngine;

public class Grapple : MonoBehaviour
{

    [SerializeField]
    public GrapplePoint currentGrapplePoint;

    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    public bool hasGrapple = false;

    private float pullBoostAmount = 2000;

    private LineRenderer grappleLine;

    private Vector3 lineStartLoc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grappleLine = gameObject.AddComponent<LineRenderer>();

        // Set the material
        //lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Set the color
        grappleLine.startColor = Color.red;
        grappleLine.endColor = Color.green;

        // Set the width
        grappleLine.startWidth = 0.2f;
        grappleLine.endWidth = 0.2f;

        // Set the number of vertices
        grappleLine.positionCount = 2;
        
    }


    public void pull()
    {
        //calculate direction of grapple point
        //apply force in that direction to player RB
        Vector2 grapplePointLoc = currentGrapplePoint.gameObject.transform.localPosition;
        Vector2 playerLoc = myRigidBody.transform.localPosition;
        Vector2 directionToMove = grapplePointLoc - playerLoc;
        myRigidBody.AddForce(directionToMove.normalized * pullBoostAmount);
    }

    //THis is here until the bug with onTriggerExit is fixed
    public void pull(GrapplePoint grapplePoint)
    {
        //calculate direction of grapple point
        //apply force in that direction to player RB
        Vector2 grapplePointLoc = grapplePoint.gameObject.transform.localPosition;
        Vector2 playerLoc = myRigidBody.transform.localPosition;
        Vector2 directionToMove = grapplePointLoc - playerLoc;
        myRigidBody.AddForce(directionToMove.normalized * pullBoostAmount);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GrapplePoint")
        {
            currentGrapplePoint = collision.GetComponent<GrapplePoint>();
            lineStartLoc = currentGrapplePoint.transform.localPosition;
        }
    }

    //There is a bug where this runs when getting on to the grapple, despite still being in the trigger, 
    private void OnTriggerExit2D(Collider2D collision)
    {
          if (collision.gameObject.tag == "GrapplePoint")
        {
            currentGrapplePoint = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*
        this code will allow for the drawing of the grapple point 
        due to other issues with onTriggerExit it doesn't draw when on the grapple but does when you are below it
        if (lineStartLoc != null)
        {
            grappleLine.SetPosition(0, lineStartLoc);
            grappleLine.SetPosition(1, myRigidBody.transform.localPosition); 
        }
        */

    }
}
