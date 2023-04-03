using System;
using UnityEngine;

namespace FastPlayer
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5;
        [SerializeField] private float _mouseSensitivity = 1;
        [SerializeField] private float _jumpHeight = 2;
        [SerializeField] private float _gravity = -9.81f;
        [Space]
        [SerializeField] private float _rotationSpeed = 10;

        private CameraController _cameraController;
        private CharacterController _charController;
        private Vector3 _previousInput;
        private Vector3 _playerVelocity;
        private float _angle;
        private bool _isCanJump;

        private void SetMove(Vector3 value) => _previousInput = value;

        private void Awake()
        {
            _charController = GetComponent<CharacterController>();
            _cameraController = GetComponent<CameraController>();
            _cameraController.SetSensitivity(_mouseSensitivity);
            InputHandler.OnMove += SetMove;
            InputHandler.OnJumpButtonDown += Jump;
        }

        private void Update()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            Vector3 forward = _cameraController.Forward;
            Vector3 right = _cameraController.Right;

            forward.y = 0;
            right.y = 0;

            var move = forward * _previousInput.z + right * _previousInput.x;

            _playerVelocity.y += _gravity * Time.deltaTime;
            if (_charController.isGrounded)
            {
                _isCanJump = true;
                _playerVelocity.y = 0;
            }

            _charController.Move(_playerVelocity * Time.deltaTime);
            _charController.Move(move.normalized * _moveSpeed * Time.deltaTime);
        }

        private void Jump()
        {
            if (!_isCanJump) return;

            _isCanJump = false;
            _playerVelocity.y = 0;
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3f * _gravity);
        }

        private void Rotate()
        {
            if (_previousInput != Vector3.zero)
            {
                var rotInput = _charController.velocity;
                _angle = (float)Math.Atan2(rotInput.x, rotInput.z) * Mathf.Rad2Deg;
            }

            transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    Quaternion.Euler(0, _angle, 0),
                    Time.deltaTime * _rotationSpeed);
        }
    }
}
