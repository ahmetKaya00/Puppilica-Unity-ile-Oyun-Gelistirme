using System.Xml.Serialization;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovemenController : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _walkSpeed = 4f;
    [SerializeField] private float _sprintSpeed = 7f;
    [SerializeField] private float _jumpHeight = 1.5f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.25f;
    [SerializeField] private LayerMask _groundMask;

    private Vector2 _moveInput;

    private Vector3 _verticalVelocity;

    private bool _isGrounded;

    private void Reset()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Awake()
    {
        if (_controller == null)
        {
            _controller = GetComponent<CharacterController>();
        }
    }

    private void Update()
    {
        HandleGroundCheck();

        ReadJoystickInput();

        HandleHorizontalMovement();

        HandleGravity();
    }

    private void ReadJoystickInput()
    {
        if(_joystick != null)
        {
            _moveInput.x = _joystick.Horizontal;
            _moveInput.y = _joystick.Vertical;
        }
    }

    private void HandleGroundCheck()
    {
        Vector3 checkPosition = _groundCheck != null ? _groundCheck.position : transform.position;

        _isGrounded = Physics.CheckSphere(checkPosition, _groundDistance, _groundMask);
        if (_isGrounded && _verticalVelocity.y < 0)
        {
            _verticalVelocity.y = -2f;
        }
    }

    private void HandleHorizontalMovement()
    {
        Vector3 moveDirection = (transform.right * _moveInput.x + transform.forward * _moveInput.y);

        bool isSprinting = _moveInput.magnitude > 0.85f; 

        float currentSpeed = isSprinting ? _sprintSpeed : _walkSpeed;

        _controller.Move(moveDirection * (currentSpeed * Time.deltaTime));
    }

    private void HandleGravity()
    {
        _verticalVelocity.y += _gravity * Time.deltaTime;
        _controller.Move(_verticalVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _verticalVelocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }

}
