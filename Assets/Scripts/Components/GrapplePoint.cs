using NUnit.Framework.Constraints;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{

    [SerializeField]
    private DistanceJoint2D _myJoint;
    [SerializeField]
    public SpriteRenderer _mySpriteRenderer;

    public void SetAwake()
    {
        gameObject.SetActive(true);
    }

    //Attatches a rigidbody to the distance joint
    public void Attatch(Rigidbody2D RbToAttatch)
    {
        _myJoint.connectedBody = RbToAttatch;
    } 
    
    //Detatches a rigidbody to the distance joint
    public void Detatch()
    {
        _myJoint.connectedBody = null;
    }

    //Enables the sprite to show the player they can interact with the grapple point
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _mySpriteRenderer.enabled = true;
        }
    }
    
    //Disables the sprite when the player is no longer in interaction range
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _mySpriteRenderer.enabled = false;
        }
    }

}
