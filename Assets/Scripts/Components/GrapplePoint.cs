using NUnit.Framework.Constraints;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{

    [SerializeField]
    private DistanceJoint2D myJoint;
    


    public void Awake()
    {
        myJoint = GetComponent<DistanceJoint2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void attatch(Rigidbody2D RbToAttatch)
    {
        myJoint.connectedBody = RbToAttatch;    
        
       
    } 

    public void detatch()
    {
        myJoint.connectedBody = null;     
    } 
    // Update is called once per frame
    void Update()
    {
        
    }
}
