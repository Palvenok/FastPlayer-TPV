using System;
using UnityEngine;

namespace FastPlayer
{
    public class InputHandler : MonoBehaviour
    {
        public static event Action OnShootButtonDown;
        public static event Action OnJumpButtonDown;
        public static event Action<Vector3> OnMove;
        public static event Action<Vector3> OnMouseMove;

        [SerializeField] private string _horizontalMoveAxis = "Horizontal";
        [SerializeField] private string _verticalMoveAxis = "Vertical";
        [SerializeField] private string _jumpButton = "Jump";
        [Space]
        [SerializeField] private string _horizontalShootAxis = "Mouse X";
        [SerializeField] private string _verticalShootAxis = "Mouse Y";
        [SerializeField] private string _shootButton = "Fire1";

        private Vector3 _moveInput;
        private Vector3 _mouseInput;

        private void Update()
        {
            _moveInput.x = Input.GetAxisRaw(_horizontalMoveAxis);
            _moveInput.z = Input.GetAxisRaw(_verticalMoveAxis);

            _mouseInput.x = Input.GetAxis(_horizontalShootAxis);
            _mouseInput.y = Input.GetAxis(_verticalShootAxis);

            if (Input.GetButtonDown(_shootButton)) OnShootButtonDown?.Invoke();
            if (Input.GetButtonDown(_jumpButton)) OnJumpButtonDown?.Invoke();

            OnMove?.Invoke(_moveInput);
            OnMouseMove?.Invoke(_mouseInput);
        }

        private void OnDestroy()
        {
            OnShootButtonDown = null;
            OnJumpButtonDown = null;
            OnMove = null;
            OnMouseMove = null;
        }
    }
}
