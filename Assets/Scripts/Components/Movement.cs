using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;

    #region Horizontal movement 
    [SerializeField]
    public float currentMoveSpeedCap = 10;
    public float defaultcurrentMoveSpeedCap = 10;
    public float moveSpeed = 5;
    public float direction;

    [SerializeField]
    public float speedIncrease = 2;
    public float maxMoveSpeedCap = 30;
    #endregion

    #region Jump
    [SerializeField]
    public float jumpHeight = 400;
    [SerializeField]
    public float wallJumpSpeed = 100;
    [SerializeField]
    public float wallSpeedBoost = 100;

    [SerializeField]
    public bool canDoubleJump = false;
    public bool hasDoubleJumpAbility = false;
    #endregion

    #region Raycast 
    [SerializeField]
    float rayCastDistanceFloor = 1f;
    [SerializeField]
    float rayCastDistanceWall = 0.5f;
    [SerializeField]
    LayerMask wallCollisionLM;
    [SerializeField]
    LayerMask groundCollisionLM;
    #endregion


    #region  Sprite
    SpriteRenderer spriteRenderer;
    #endregion

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetDirection(InputAction.CallbackContext inputContext)
    {
        if (inputContext.canceled)
        {
            
            return;
        }
        direction = inputContext.ReadValue<Vector2>().x;
        if (direction == 1)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    public void SetDirection()
    {
        direction *= -1;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }


    public void HorizontalMove()
    {
        rb.AddForce(new Vector2(direction * moveSpeed, 0));
    }

    public void Jump()
    {
        rb.linearVelocityY = 0;
        rb.AddForce(new Vector2(0, jumpHeight));
    }

    public void WallJump()
    {
        rb.linearVelocityY = 0;
        SetDirection();
        rb.AddForce(new Vector2(direction * wallJumpSpeed, jumpHeight*1.5f));
        
    }

    public void Slide()
    {
        currentMoveSpeedCap += speedIncrease;
        
    }

    public void resetMaxMoveSpeed()
    {
        currentMoveSpeedCap = defaultcurrentMoveSpeedCap;
    }


    public void HitWall()
    {
        if (rb.linearVelocity.y > 0)
        {

            rb.AddForce(new Vector2(0, rb.linearVelocity.x * wallSpeedBoost));
        }
    }

    public void FixedUpdate()
    {
        if (currentMoveSpeedCap > maxMoveSpeedCap)
        {
            currentMoveSpeedCap = maxMoveSpeedCap;
        }


        if (rb.linearVelocity.x > currentMoveSpeedCap || rb.linearVelocity.x < -currentMoveSpeedCap )
        {
            rb.linearVelocity = new Vector2(currentMoveSpeedCap * direction, rb.linearVelocity.y);

        }

    }


    public bool WallCollisionCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, rayCastDistanceWall, wallCollisionLM);
        Debug.DrawRay(transform.position, Vector2.right * direction * rayCastDistanceWall, Color.red);
        if (hit)
        {
            return true;
        }
        return false;
    }
    
    public bool GroundCollisionCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayCastDistanceFloor, groundCollisionLM);
        Debug.DrawRay(transform.position, Vector2.down * rayCastDistanceFloor, Color.green );
        if (hit)
        {
            return true;
        }
        return false;
    }
}
