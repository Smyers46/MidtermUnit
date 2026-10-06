using Unity.VisualScripting;
using UnityEngine;

namespace Midterm
{

    [RequireComponent (typeof (CharacterController))]
    public class PlayerMovementBehaviour : MonoBehaviour
    {

        [SerializeField] private PlayerInput _input;

        [Header("PlayerMovement")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private float _sprintMultiplier = 1.5f;

        [Header("GroundCheck")]
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private float _groundCheckDistance = 0.2f;

        private CharacterController _characterController;
        private Vector3 _playerVelocity;

        public bool isGrounded { get; private set; }

        private float _moveMultiplier = 1f;

        void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            GroundCheck();
            MovePlayer();
        }

        private void GroundCheck()
        {
            isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckDistance, _groundMask);
        }

        private void MovePlayer()
        {
            // Sprint
            _moveMultiplier = _input.sprintHeld ? _sprintMultiplier : 1f;

            // Movement
            Vector3 move = transform.forward * _input.verticalInput + transform.right * _input.horizontalInput;

            _characterController.Move(move * _moveSpeed * _moveMultiplier * Time.deltaTime);

            // Keep Player Grounded
            if (isGrounded && _playerVelocity.y < 0)
            {
                _playerVelocity.y = -2f;
            }

            //Gravity
            _playerVelocity.y += _gravity * Time.deltaTime;

            // Vertical Movement
            _characterController.Move(_playerVelocity * Time.deltaTime);

        }

        public void SetYVelocity(float value)
        {
            _playerVelocity.y = value;
        }

        public float GetForwardSpeed()
        {
            return _input.verticalInput * _moveSpeed * _moveMultiplier;
        }

        public void ResetToSpawn(Transform spawnTransform)
        {
            CharacterController controller = GetComponent<CharacterController>();

            controller.enabled = false;

            transform.position = spawnTransform.position;
            transform.rotation = spawnTransform.rotation;

            controller.enabled = true;
        }

    }
}
