using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;

    #region Horizontal movement 
    [SerializeField]
    public float currentMoveSpeedCap = 15;
    [SerializeField]
    private float _defaultMoveSpeedCap = 15;
    [SerializeField]
    private float _moveSpeed = 5;
    [SerializeField]
    public float Direction;

    [SerializeField]
    private float _moveSpeedAir;

    [SerializeField]
    private Vector2 _WallJumpForce;
    [SerializeField]
    private float _fallSpeedIncrease;


    [SerializeField]
    private float _wallJumpDirection;

    [SerializeField]
    //The amount the current speed cap will increase by when sliding
    private float _speedIncrease = 2;
    [SerializeField]
    public float maxMoveSpeedCap = 20;
    #endregion

    #region Jump
    [SerializeField]
    public float jumpHeight = 400;

    [SerializeField]
    private float _wallSpeedBoost = 100;

    [SerializeField]
    private Vector2 _grappleJumpHeight;

    [SerializeField]
    public bool canDoubleJump = false;
    [SerializeField]
    public bool hasDoubleJumpAbility = false;
    #endregion

    [SerializeField]
    private float _wallBoostAmount = 0;


    #region  Sprite
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    #endregion

    //Sets the direction the game object using the component is moving on the X axis and ensures the sprite is facing the correct way
    public void SetDirection(float direction)
    {
        Direction = direction;
        if (Direction == 1)
        {
            _spriteRenderer.flipX = false;
        }
        else if (Direction == -1)
        {
            _spriteRenderer.flipX = true;
        }

        if (direction == 0)
        {
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(ResetWallBoostAmount());
            }
             return;
        }
        // When the player jumps off the wall it needs to be in the opposite direction to the wall
        _wallJumpDirection = direction * -1;


    }
    // Resets the amount of _wallBoostAmount if the player hasn't been moving for 2 seconds
    public IEnumerator ResetWallBoostAmount()
    {
        yield return new WaitForSecondsRealtime(2);
        if (rb.linearVelocityX > -2 && rb.linearVelocityX < 2)
        {
            _wallBoostAmount = 0;
        }

    }
    // Moves the game object along the X axis based on the Direction variable when on the ground
    public void HorizontalMove()
    {
        rb.AddForce( new(_moveSpeed * Direction,0));
        _wallBoostAmount += 0.2f;
    }

    // Moves the game object along the X axis based on the Direction variable, has a different multiplier for the change of speed as the object is in the air 
    public void HorizontalMoveInAir()
    {
        rb.AddForce(new Vector2(Direction * _moveSpeedAir, 0));
        _wallBoostAmount += 0.2f;
    }
    // Applies force to the game object to make it jump
    public void Jump()
    {
        rb.linearVelocityY = 0;
        rb.AddForce(new Vector2(0, jumpHeight));
    }

    // For when the player jumps off of the grapple this helps move them horizontally as well as vertically
    public void JumpFromGrapple()
    {
        rb.linearVelocityY = 0;
        //_grappleJumpHeight.x*Direction
        rb.AddForce(new Vector2(rb.linearVelocityX * _grappleJumpHeight.x, _grappleJumpHeight.y));
    }
    //Makes the game object fall faster if they remain in the air 
    public void AddtionalGravity()
    {
        rb.AddForceY(_fallSpeedIncrease);
    }
    // Applies force to move the player away from the wall and up
    public void WallJump()
    {
        rb.linearVelocityY = 0;
        rb.linearVelocityX = 0;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
        rb.AddForce(new Vector2(_wallJumpDirection * _WallJumpForce.x, _WallJumpForce.y));
        _wallJumpDirection *= -1;
        _wallBoostAmount = 10;
    }
    //Incerease the current move speed cap while the player is sliding
    public void Slide()
    {
        currentMoveSpeedCap += _speedIncrease;  
    }
    //When the player stops moving or turns on the ground reset thier move speed cap to its default amount
    public void resetMaxMoveSpeed()
    {
        currentMoveSpeedCap = _defaultMoveSpeedCap;
    }

    // When a hit work out if an upwards force should be applied to allow the player to slide them up the wall
    public void HitWall()
    {

        if (rb.linearVelocity.y > -3.5)
        {
            if (_wallBoostAmount > 10)
            {
                _wallBoostAmount = 10;
            }
            rb.linearVelocity = new(0, _wallBoostAmount  * _wallSpeedBoost);
        }
    }

    /*
    causes too much of a slow down when in air
        public void Brake(InputAction.CallbackContext inputContext)
        {
            if (inputContext.started)
            {
                currentMoveSpeedCap /= 2;
                _defaultMoveSpeedCap /= 2;
            }
            if (inputContext.canceled)
            {
                currentMoveSpeedCap *= 2;
                _defaultMoveSpeedCap *= 2;
            }
        }
    */
    //Limits the speed the game object can move
    public void CheckMoveSpeed()
    {
        if ((rb.linearVelocity.x > currentMoveSpeedCap || rb.linearVelocity.x < -currentMoveSpeedCap) && Direction != 0)
        {
            rb.linearVelocityX = Vector2.ClampMagnitude(rb.linearVelocity, currentMoveSpeedCap).x;
        }
    }
    
    public void FixedUpdate()
    {
        CheckMoveSpeed();

        if (currentMoveSpeedCap > maxMoveSpeedCap)
        {
            currentMoveSpeedCap = maxMoveSpeedCap;
        }
    }


}
