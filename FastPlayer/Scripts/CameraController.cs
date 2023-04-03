using UnityEngine;

namespace FastPlayer
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraHolder;
        [SerializeField] private Transform _cameraHolderPoint;
        [Space]
        [SerializeField] private bool _useCustomCamera;
        [SerializeField] private Camera _customCamera;

        private Camera _camera;
        private Vector2 _curentRottation;
        private Vector2 _input;
        private float _mouseSensitivity;

        public Vector3 Forward => _camera.transform.forward;
        public Vector3 Right => _camera.transform.right;

        public void Awake()
        {
            InputHandler.OnMouseMove += SetInput;
            _camera = _useCustomCamera ? _customCamera : Camera.main;
        }

        public void SetSensitivity(float value) => _mouseSensitivity = value;

        private void SetInput(Vector3 value) => _input = value;

        private void LateUpdate()
        {
            _curentRottation.x += _input.x * _mouseSensitivity;
            _curentRottation.y -= _input.y * _mouseSensitivity;
            _curentRottation.x = Mathf.Repeat(_curentRottation.x, 360);
            _curentRottation.y = Mathf.Clamp(_curentRottation.y, -35, 30);

            _cameraHolder.transform.rotation = Quaternion.Euler(_curentRottation.y, _curentRottation.x, 0);

            _camera.transform.position = _cameraHolderPoint.transform.position;
            _camera.transform.LookAt(transform.position);
        }
    }
}
