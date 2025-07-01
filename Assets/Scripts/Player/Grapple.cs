using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    [SerializeField]
    public GrapplePoint currentGrapplePoint;

    [SerializeField]
    private Rigidbody2D myRigidBody;

    private float pullBoostAmount = 2000;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        }
    }


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
        
    }
}
