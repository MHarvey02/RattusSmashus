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
    private float _rayCastDistanceWhileOnWall = 1f;
    [SerializeField]
    private LayerMask _wallCollisionLM;
    
    [SerializeField]
    private LayerMask _groundCollisionLM;
    #endregion

    //Sets direction to draw the horizontal raycast
    public void SetDirection(InputAction.CallbackContext inputContext)
    {
        if (inputContext.canceled) { return; }
        _direction = inputContext.ReadValue<Vector2>().x;
    }

    //Used for when the player jumps off of a wall
    public void ChangeDirection(float newDirection)
    {
        _direction = newDirection;
    }

    //Draw a raycast to check if the player has hit a wall
    public bool IsTouchingWall()
    {
        RaycastHit2D hitTop = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector2.right * _direction, _rayCastDistanceWall, _wallCollisionLM);
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), Vector2.right * _direction * _rayCastDistanceWall, Color.red);

        RaycastHit2D hitMid = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector2.right * _direction, _rayCastDistanceWall, _wallCollisionLM);
        Debug.DrawRay(transform.position + new Vector3(0, -0.5f, 0), Vector2.right * _direction * _rayCastDistanceWall, Color.red);

        RaycastHit2D hitBottom = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector2.right * _direction, _rayCastDistanceWall, _wallCollisionLM);
        Debug.DrawRay(transform.position + new Vector3(0, -0.5f, 0), Vector2.right * _direction * _rayCastDistanceWall, Color.red);

        if (hitTop && hitMid && hitBottom)
        {
            return true;
        }
        return false;
    }

    //While the player is on the wall draw a longer raycast to give more leniency on the wall jump if they hold away from the wall
    public bool IsOnStillWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * _direction * -1, _rayCastDistanceWhileOnWall, _wallCollisionLM);
        Debug.DrawRay(transform.position, Vector2.right * _direction * -1 * _rayCastDistanceWhileOnWall, Color.black);
        if (hit)
        {
            return true;
        }
        return false;
    }


    //Draw two raycasts down to check if the player is on the ground
    public bool IsTouchingGround()
    {

        //Three raycasts are being drawn here so it can identify the front, back and middle of the player to tell if they are on the ground
        //* _direction to make the ray cast behind the player the furthest from them 
        RaycastHit2D hitBehind = Physics2D.Raycast(transform.position - new Vector3(0.8f * _direction, 0.5f, 0),Vector2.down, _rayCastDistanceFloor, _groundCollisionLM);
        Debug.DrawRay(transform.position - new Vector3(0.8f * _direction, 0.5f, 0), Vector2.down * _rayCastDistanceFloor, Color.green);

        RaycastHit2D hitFront = Physics2D.Raycast(transform.position + new Vector3(0.3f * _direction, -0.5f, 0),Vector2.down, _rayCastDistanceFloor, _groundCollisionLM);
        Debug.DrawRay(transform.position + new Vector3(0.4f* _direction, -0.3f, 0), Vector2.down * _rayCastDistanceFloor, Color.green);
        //For when the player is stood of a thin platform
        RaycastHit2D hitMid = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f, 0), Vector2.down, _rayCastDistanceFloor, _groundCollisionLM);
        Debug.DrawRay(transform.position + new Vector3(0, -0.5f, 0), Vector2.down * _rayCastDistanceFloor, Color.green);

        if (hitFront || hitBehind || hitMid)
        {
            return true;
        }

        return false;
    }
}
