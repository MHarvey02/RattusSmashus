using UnityEngine;

//Create a wall that kill the player if it touches them
// It needs to move in a set direction 
public class DeathWall : MonoBehaviour
{
    [SerializeField]
    private Vector2 _speed = new(1, 0);
    [SerializeField]
    private Rigidbody2D _myRB;
    void Start()
    {
        _myRB.linearVelocity = _speed;
    }

}
