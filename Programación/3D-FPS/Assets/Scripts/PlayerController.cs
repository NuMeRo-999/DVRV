using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Importa la biblioteca UI
using _3d_fps.Manager;
using TMPro;

namespace _3d_fps.PlayerControl
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float AnimBlendSpeed = 8.9f;
        [SerializeField] private Transform CameraRoot;
        [SerializeField] private Transform Camera;
        [SerializeField] private float UpperLimit = -40f;
        [SerializeField] private float BottomLimit = 70f;
        [SerializeField] private float MouseSensitivity = 21.9f;
        [SerializeField] private TextMeshProUGUI PlayerSpeedText;  // Referencia al Text UI
        private Rigidbody _playerRigidbody;
        private InputManager _inputManager;
        private Animator _animator;
        private bool _hasAnimator;
        private int _xVelHash;
        private int _yVelHash;
        private float _xRotation;

        private const float _walkSpeed = 2f;
        private const float _runSpeed = 6f;
        private Vector2 _currentVelocity;

        private void Start()
        {
            _hasAnimator = TryGetComponent<Animator>(out _animator);
            _playerRigidbody = GetComponent<Rigidbody>();
            _inputManager = GetComponent<InputManager>();

            _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity");
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void LateUpdate()
        {
            CamMovements();
        }

        private void Move()
        {
            if (!_hasAnimator) return;

            float targetSpeed = _inputManager.Run ? _runSpeed : _walkSpeed;
            Vector2 inputDirection = _inputManager.Move;

            if (inputDirection != Vector2.zero)
            {
                Vector2 inputMagnitude = inputDirection.normalized * targetSpeed;
                _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, inputMagnitude.x, AnimBlendSpeed * Time.fixedDeltaTime);
                _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, inputMagnitude.y, AnimBlendSpeed * Time.fixedDeltaTime);

                Vector3 targetVelocity = new Vector3(_currentVelocity.x, _playerRigidbody.velocity.y, _currentVelocity.y);

                var xVelDifference = (_currentVelocity.x - _playerRigidbody.velocity.x);
                var zVelDifference = (_currentVelocity.y - _playerRigidbody.velocity.z);

                _playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, zVelDifference)), ForceMode.VelocityChange);

                _animator.SetFloat(_xVelHash, _currentVelocity.x);
                _animator.SetFloat(_yVelHash, _currentVelocity.y);
            }
            else
            {
                _currentVelocity = Vector2.zero;
                _animator.SetFloat(_xVelHash, 0);
                _animator.SetFloat(_yVelHash, 0);
            }

            UpdateSpeedText();
        }

        private void CamMovements()
        {
            if (!_hasAnimator) return;

            var Mouse_X = _inputManager.Look.x;
            var Mouse_Y = _inputManager.Look.y;
            Camera.position = CameraRoot.position;

            _xRotation -= Mouse_Y * MouseSensitivity * Time.deltaTime;
            _xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);

            Camera.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            transform.Rotate(Vector3.up * Mouse_X * MouseSensitivity * Time.smoothDeltaTime);
        }

        private void UpdateSpeedText()
        {
            if (PlayerSpeedText != null)
            {
                PlayerSpeedText.text = $"Player Speed: {_playerRigidbody.velocity.magnitude:F2}\n" +
                                       $"Animation Speed X: {_currentVelocity.x:F2}\n" +
                                       $"Animation Speed Y: {_currentVelocity.y:F2}";
            }
        }
    }
}
