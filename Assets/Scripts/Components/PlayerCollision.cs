using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    private float _direction;

    #region Raycast 
    [SerializeField]
    private float rayCastDistanceFloor = 1f;
    [SerializeField]
    private float rayCastDistanceWall = 0.5f;
    [SerializeField]
    private LayerMask wallCollisionLM;
    [SerializeField]
    private LayerMask groundCollisionLM;
    #endregion
    
    public void SetDirection(InputAction.CallbackContext inputContext)
    {
        if(inputContext.canceled){ return; }
        _direction = inputContext.ReadValue<Vector2>().x;
    }

    public void ChangeDirection()
    {
        _direction *= -1;
    }
    public bool IsTouchingWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * _direction, rayCastDistanceWall, wallCollisionLM);
        Debug.DrawRay(transform.position, Vector2.right * _direction * rayCastDistanceWall, Color.red);
        if (hit)
        {
            return true;
        }
        return false;
    }
    
    public bool IsTouchingGround()
    {

        // Two raycasts are being drawn here so it can identify the front and back of the player to tell if they are on the ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position -new Vector3(0.3f,0,0), Vector2.down, rayCastDistanceFloor, groundCollisionLM);
        Debug.DrawRay(transform.position -new Vector3(0.3f,0,0), Vector2.down * rayCastDistanceFloor, Color.green );
        RaycastHit2D hitTwo = Physics2D.Raycast(transform.position + new Vector3(0.4f,0,0), Vector2.down, rayCastDistanceFloor, groundCollisionLM);
        Debug.DrawRay(transform.position + new Vector3(0.4f,0,0), Vector2.down * rayCastDistanceFloor, Color.green );

        /*
        I want to use a boxcast but the RaycaseHit2D and Debug functions take different parameters for angle, I currently cannot work out a conversion
        Quaternion quaternion = new(0, -1, 0, 0);
        RaycastHit2D boxCast = Physics2D.BoxCast(transform.position - new Vector3(0,0.7f),
        new(0.5f,0.1f),
        quaternion.eulerAngles.y,
        new(0, -1),
        10,
        groundCollisionLM);

        DebugBoxCast.SimpleDrawBox(transform.position - new Vector3(0,0.7f), new(0.5f,0.1f), new(0, -1, 0, 0), Color.red);
        */
        if (hit || hitTwo)
        {
            return true;
        }
        return false;
    }
}
