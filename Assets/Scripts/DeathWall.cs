using UnityEngine;

public class DeathWall : MonoBehaviour
{

    [SerializeField]
    private Vector2 _speed = new(1,0);
    [SerializeField]
    private Rigidbody2D _myRB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _myRB.linearVelocity = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
