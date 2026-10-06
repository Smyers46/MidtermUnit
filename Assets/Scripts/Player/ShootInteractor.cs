using UnityEngine;

namespace Midterm
{
    public class ShootInteractor : Interactor
    {
        [SerializeField] Input _inputType; //the enum from below

        private IWeaponBehavior _currentShootStrategy;

        [Header("Shoot")]
        [SerializeField] Rigidbody _bulletPrefab;
        [SerializeField] float _shootVelocity;
        [SerializeField] Transform _shootPoint;
        [SerializeField] PlayerMovementBehaviour _playerMovementBehavior;

        [SerializeField] MeshRenderer _gunRenderer;

        //private variable
        float _finalShootVelocity;

        private void Start()
        {
            SwitchWeapon(new BulletWeaponBehavior(this));
        }

        public override void Interact()
        {
            if (_inputType == Input.Primary && PlayerInput.Instance.primaryShootPressed || _inputType == Input.Secondary && PlayerInput.Instance.secondaryShootPressed)
            {
                Shoot();
            }

            // Switch to bullet weapon
            if (PlayerInput.Instance.alpha1Pressed)
            {
                SwitchWeapon(new BulletWeaponBehavior(this));
                AudioManager.Instance.PlaySFX(AudioManager.Instance.switchWeapon);
            }

            // Switch to rocket weapon
            if (PlayerInput.Instance.alpha2Pressed)
            {
                SwitchWeapon(new RocketWeaponBehavior(this));
                AudioManager.Instance.PlaySFX(AudioManager.Instance.switchWeapon);
            }
        }
        private void Shoot()
        {
            _finalShootVelocity = _playerMovementBehavior.GetForwardSpeed() + _shootVelocity; //adds velocity of the player

            _currentShootStrategy.FireWeapon();

            if (_currentShootStrategy is BulletWeaponBehavior)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.shootBullet);
            }
            else if (_currentShootStrategy is RocketWeaponBehavior)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.shootRocket);
            }
        }

        // - Get the shoot start position and rotation
        public Transform GetShootPoint()
        {
            return _shootPoint;
        }

        // - Get the shoot velocity
        public float GetShootVelocity()
        {
            return _shootVelocity;
        }

        public MeshRenderer GetWeaponRenderer()
        {
            return _gunRenderer;
        }

        public void SwitchWeapon(IWeaponBehavior newWeapon)
        {
            //responsible for determining which weapon used.

            _currentShootStrategy = newWeapon;
        }

    }

    /// <summary>
    /// types of shooting that the player has. Enum will select which type of bullet
    /// </summary>
    public enum Input
    {
        Primary,
        Secondary
    }
}
