/*using UnityEngine;
using UnityEngine.InputSystem;

namespace Midterm
{
    public class PlayerWeapon : MonoBehaviour
    {
        private I_WeaponBehaviour _currentWeapon;
        public GameObject shotPoint;

        public GameObject weaponReference;

        public GameObject rocketProjectile;
        public GameObject bulletProjectile;

        [SerializeField] InputActionReference SwitchProjectileWeapon;
        [SerializeField] InputActionReference SwitchRayCastWeapon;

        private void Start()
        {
            SwitchWeapon(new ProjectileWeaponBehaviour(this));
        }

        //private void Update()
        {
            // Left mouse button
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) {
            // Shoot here 
            // _currentWeapon.Shoot();
            }

            // When 1 key is pressed, switch to projectile behaviour
            if (Keyboard.current != null && Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                SwitchWeapon(new ProjectileWeaponBehaviour(this));
            }

            // When 2 key is pressed, switch to raycast behaviour

            else if (Keyboard.current != null && Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                SwitchWeapon(new RayCastWeaponBehaviour(this));
            }

        }

        public void SwitchWeapon(I_WeaponBehaviour newWeapon)
        {
            _currentWeapon = newWeapon;
            Debug.Log("Switched weapon behaviour to: " + newWeapon.GetType().Name);
        }
    }
}
*/