using UnityEngine;

public class Trap : MonoBehaviour
{

    [SerializeField]
    private float _rotationSpeed = 3;

    [SerializeField]
    public FollowPath Path;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, _rotationSpeed));
        if (Path)
        {
          Path.Move();  
        }
        
        

    }
}
