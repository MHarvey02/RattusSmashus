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

    public void Awake()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Attatch(Rigidbody2D RbToAttatch)
    {
        _myJoint.connectedBody = RbToAttatch;     
    } 

    public void Detatch()
    {
        _myJoint.connectedBody = null;     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           _mySpriteRenderer.enabled = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           _mySpriteRenderer.enabled = false; 
        }
    }

    // Update is called once per frame
    public void FixedUpdateUpdate()
    {
        _mySpriteRenderer.gameObject.transform.Rotate(0, 0, 5 * Time.deltaTime);
    }
    
}
