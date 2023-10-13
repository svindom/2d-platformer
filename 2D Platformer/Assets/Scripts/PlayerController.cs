using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D _rb;
    Animator _animator;

    [Header("Move info")]
    public float _moveSpeed;
    public float _jumpForce;

    float _movingInput;

    bool _canDoubleJump = true;

    [Header("Collision info")]
    public LayerMask _whatIsGround;
    public float _groundCheckDistance;
    bool _isGrounded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        bool isMoving = _rb.velocity.x != 0;
        _animator.SetBool("IsMoving", isMoving);

        CollisionChecks();
        InputChecks();

        if (_isGrounded)
        {
            _canDoubleJump = true;
        }
        Move();
    }




    private void InputChecks()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
        }
    }


    private void JumpButton()
    {
        if (_isGrounded)
        {
            Jump();
        }
        else if (_canDoubleJump) 
        {
            _canDoubleJump = false;
            Jump();
        }
    }

    private void Move()
    {
        _movingInput = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector3(_moveSpeed * _movingInput, _rb.velocity.y, 0);
    }

    private void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _jumpForce, 0);
    }

    private void CollisionChecks()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector3.down, _groundCheckDistance, _whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - _groundCheckDistance, transform.position.z));
    }
}
