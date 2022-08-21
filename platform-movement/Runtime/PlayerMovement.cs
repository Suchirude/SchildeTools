using System.Collections;
using UnityEngine;


namespace SchildeTools.PlatformMovement
{
    public class PlayerMovement : MonoBehaviour 
    {
        [Header("PlayerValues")]
        [SerializeField] private float playerSpeed;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float coyoteTime;
        [SerializeField] private float jumpBuffer;
        [SerializeField] private float lowJumpMultiplier;
        [SerializeField] private float fallMultiplier;
        
        
        [Header("Collisions")]
        [SerializeField] private LayerMask whatIsGround;
        
        
        private float _jumpBufferTimer;
        private float _coyoteTimer;
        private float _inputX;
        private Rigidbody2D _rigidbody;
        private bool _isJumping;
        private Collider2D _playerCollider;
        private bool _canJump;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _playerCollider = GetComponent<Collider2D>();
        }
        
        private void Update()
        {
            _inputX = Input.GetAxisRaw("Horizontal");
        
            _canJump = _coyoteTimer > 0 && _jumpBufferTimer > 0 && !_isJumping;
        
            if(_canJump)
            {
                Jump();
        
                _jumpBufferTimer = 0f;
        
                StartCoroutine(JumpCooldown());
            }
        
            BetterJump();
            CoyoteTime();
            JumpBuffer();
        }
        
        private void FixedUpdate()
        {
            MovePlayer();
        }
        
        private void MovePlayer()
        {
            _rigidbody.velocity = new Vector2(_inputX * playerSpeed * Time.deltaTime, _rigidbody.velocity.y);
        }
        
        private void Jump()
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpHeight);
        }
        
        
        private void BetterJump()
        {
            if (_rigidbody.velocity.y < 0)
            {
                _rigidbody.velocity += Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime * Vector2.up;
            }
            else if (_rigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                _rigidbody.velocity +=  Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime * Vector2.up;
            }
        }
        
        private void CoyoteTime()
        {
            if (IsGrounded())
            {
                _coyoteTimer = coyoteTime;
            }
            else
            {
                _coyoteTimer -= Time.deltaTime;
            }
        }
        
        private void JumpBuffer()
        {
            if (Input.GetButtonDown("Jump"))
            {
                _jumpBufferTimer = jumpBuffer;
            }
            else
            {
                _jumpBufferTimer -= Time.deltaTime;
            }
        }
        
        private bool IsGrounded()
        {
            return Physics2D.BoxCast(_playerCollider.bounds.center, _playerCollider.bounds.size, 0f, Vector2.down, .1f, whatIsGround);
        }
        
        
        
        private IEnumerator JumpCooldown()
        {
            _isJumping = true;
            yield return new WaitForSeconds(0.4f);
            _isJumping = false;
        }
    }
}       