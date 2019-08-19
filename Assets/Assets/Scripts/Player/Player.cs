using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{

    //config variables
    [SerializeField] private float _horizontalSpeed = 5f;
    [SerializeField] private float _jumpSpeed = 5f;
    [SerializeField] private float _jumpSpeedWithBootsY = 15f;
    [SerializeField] private float _jumpSpeedWithBootsX = 8f;
    [SerializeField] private int _health = 4;
    [SerializeField] private float _delay = 3f;
    //private float _verticalSpeed = 5f;//delete this variable later. just experimenting mobile inputs

    public int Health { get; set; }
    public int diamonds;

    //cached
    private Rigidbody2D _myRigidBody2D;
    private PlayerAnimations _playerAnimations;
    private Collider2D _myCollider2D;

    // Use this for initialization
	void Start () {
        _myRigidBody2D = GetComponent<Rigidbody2D>();
        _playerAnimations = GetComponent<PlayerAnimations>();
        _myCollider2D = GetComponent<Collider2D>();
        Health = _health;
	}
	
	// Update is called once per frame
	void Update () {
        MoveHorizontally();
        Jump();
        Attack();
	}

    private void Jump()
    {
        Vector2 jumpVelocityToAdd;
        IsGrounded();
        if (Input.GetButtonDown("Jump")||CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                if (GameManager.Instance.HasBootsOfFlight)
                {
                    jumpVelocityToAdd = new Vector2(_jumpSpeedWithBootsX, _jumpSpeedWithBootsY);
                }
                else
                {
                    jumpVelocityToAdd = new Vector2(0f, _jumpSpeed);
                }
                 
                _myRigidBody2D.velocity += jumpVelocityToAdd;
                _playerAnimations.PlayJumpAnimation(IsGrounded());
                return;
            }

        }
        _playerAnimations.PlayJumpAnimation(false);

    }

    private void MoveHorizontally()
    {

		//float horizontalInput = Input.GetAxisRaw("Horizontal");
		float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        //float verticalInput = CrossPlatformInputManager.GetAxis("Vertical"); //this was a mobile input experiment. can delete
        //_myRigidBody2D.velocity = new Vector2(horizontalInput * _horizontalSpeed, verticalInput*_verticalSpeed); //this was a mobile input experiment. can delete
        _myRigidBody2D.velocity = new Vector2(horizontalInput * _horizontalSpeed, _myRigidBody2D.velocity.y);
        _playerAnimations.PlayRunAnimation(_myRigidBody2D.velocity.x);
    }

    private void Attack()
    {
        
        if ((CrossPlatformInputManager.GetButtonDown("Attack"))&&IsGrounded())
        {
            _playerAnimations.PlayAttackAnimation();
        }
    }

    private bool IsGrounded()
    {
        bool _isGrounded = false;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down, Color.green);

        if (hitInfo.collider != null)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }

        return _isGrounded;
    }

    public void Damage()
    {
        Health--;
        UIManager.Instance.UpdateLives(Health);
        //Set taking damagetrigger here to play hit animation for player
        //_playerAnimations.PlayHitAnimation();
        if (Health < 1)
        {
            _playerAnimations.PlayDeathAnimation();
            StartCoroutine(LoadMainMenuAfterDelay());
            //_myCollider2D.enabled = false;
            //_myRigidBody2D.gravityScale=0;
        }

    }

    public void AddGems(int amount)
    {
        diamonds+=amount;
        UIManager.Instance.UpdatePlayerGemCount(diamonds);
    }

    IEnumerator LoadMainMenuAfterDelay()
    {
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(0);
    }


}
