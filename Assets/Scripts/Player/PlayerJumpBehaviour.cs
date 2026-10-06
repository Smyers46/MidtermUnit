using UnityEngine;

namespace Midterm
{

    [RequireComponent(typeof(PlayerMovementBehaviour))]

    public class PlayerJumpBehaviour : Interactor
    {

        [SerializeField] private float _jumpVelocity;

        private PlayerMovementBehaviour _playerMovementBehaviour;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();
        }

        public override void Interact()
        {
            if (PlayerInput.Instance.jumpPressed && _playerMovementBehaviour.isGrounded)
            {
                _playerMovementBehaviour.SetYVelocity(_jumpVelocity);
            }
        }

    }
}
