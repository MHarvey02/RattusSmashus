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
    public float defaultMoveSpeedCap = 15;
    public float moveSpeed = 5;
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
    public float speedIncrease = 2;
    [SerializeField]
    public float maxMoveSpeedCap = 20;
    #endregion

    #region Jump
    [SerializeField]
    public float jumpHeight = 400;
    [SerializeField]
    public float wallJumpSpeed = 100;
    [SerializeField]
    public float wallSpeedBoost = 100;

    [SerializeField]
    private Vector2 _grappleJumpHeight;

    [SerializeField]
    public bool canDoubleJump = false;
    public bool hasDoubleJumpAbility = false;
    #endregion

    [SerializeField]
    private float _wallBoostAmount = 0;




    #region  Sprite
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    #endregion

    public void Awake()
    {

    }

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


        _wallJumpDirection = direction * -1;


    }

    public IEnumerator ResetWallBoostAmount()
    {
        yield return new WaitForSecondsRealtime(2);
        if (rb.linearVelocityX > -2 && rb.linearVelocityX < 2)
        {
            _wallBoostAmount = 0;
        }

    }

    public void HorizontalMove()
    {
        rb.linearVelocityX = currentMoveSpeedCap * Direction;
        _wallBoostAmount += 0.2f;
    }

    public void HorizontalMoveInAir()
    {
        rb.AddForce(new Vector2(Direction * _moveSpeedAir, 0));
        _wallBoostAmount += 0.2f;
    }

    public void Jump()
    {
        rb.linearVelocityY = 0;
        rb.AddForce(new Vector2(0, jumpHeight));
    }

    public void JumpFromGrapple()
    {
        rb.linearVelocityY = 0;
        rb.AddForce(new Vector2(_grappleJumpHeight.x*Direction, _grappleJumpHeight.y));
    }
    public void AddtionalGravity()
    {
        rb.AddForceY(_fallSpeedIncrease);
    }

    public void WallJump()
    {
        rb.linearVelocityY = 0;
        rb.linearVelocityX = 0;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
        rb.AddForce(new Vector2(_wallJumpDirection * _WallJumpForce.x, _WallJumpForce.y));
        _wallJumpDirection *= -1;
        _wallBoostAmount = 10;
    }

    public void Slide()
    {
        currentMoveSpeedCap += speedIncrease;  
    }

    public void resetMaxMoveSpeed()
    {
        currentMoveSpeedCap = defaultMoveSpeedCap;
    }


    public void HitWall()
    {

        if (rb.linearVelocity.y > -3.5)
        {
            if (_wallBoostAmount > 10)
            {
                _wallBoostAmount = 10;
            }
            rb.linearVelocity = new(0, _wallBoostAmount  * wallSpeedBoost);
        }
    }

/*
causes too much of a slow down when in air
    public void Brake(InputAction.CallbackContext inputContext)
    {
        if (inputContext.started)
        {
            currentMoveSpeedCap /= 2;
            defaultMoveSpeedCap /= 2;
        }
        if (inputContext.canceled)
        {
            currentMoveSpeedCap *= 2;
            defaultMoveSpeedCap *= 2;
        }
    }
*/
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
