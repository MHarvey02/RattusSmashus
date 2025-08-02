using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    private float _direction;

    #region Raycast 
    [SerializeField]
    private float _rayCastDistanceFloor = 0.7f;
    [SerializeField]
    private float _rayCastDistanceWall = 0.5f;
    [SerializeField]
    private LayerMask _wallCollisionLM;
    [SerializeField]
    private LayerMask _groundCollisionLM;
    #endregion
    //Sets direction to draw the horizontal raycast
    public void SetDirection(InputAction.CallbackContext inputContext)
    {
        if(inputContext.canceled){ return; }
        _direction = inputContext.ReadValue<Vector2>().x;
    }
    //Used for when the player jumps off of a wall
    public void ChangeDirection()
    {
        _direction *= -1;
    }
    //Draw a raycast to check if the player has hit a wall
    public bool IsTouchingWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector2.right * _direction, _rayCastDistanceWall, _wallCollisionLM);
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), Vector2.right * _direction * _rayCastDistanceWall, Color.red);

        RaycastHit2D hitTwo = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector2.right * _direction, _rayCastDistanceWall, _wallCollisionLM);
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), Vector2.right * _direction * _rayCastDistanceWall, Color.red);

        if (hit && hitTwo)
        {
            return true;
        }
        return false;
    }
    //Draw two raycasts down to check if the player is on the ground
    public bool IsTouchingGround()
    {

        // Two raycasts are being drawn here so it can identify the front and back of the player to tell if they are on the ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0.8f, 0, 0),
         Vector2.down, _rayCastDistanceFloor, _groundCollisionLM);
        Debug.DrawRay(transform.position - new Vector3(0.8f, 0, 0), Vector2.down * _rayCastDistanceFloor, Color.green);
        RaycastHit2D hitTwo = Physics2D.Raycast(transform.position + new Vector3(0.8f, 0, 0), Vector2.down, _rayCastDistanceFloor, _groundCollisionLM);
        Debug.DrawRay(transform.position + new Vector3(0.8f, 0, 0), Vector2.down * _rayCastDistanceFloor, Color.green);

        if (hit || hitTwo)
        {
            return true;
        }
        return false;
    }
}
