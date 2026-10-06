using UnityEngine;

namespace MidtermTuringTest
{
    public class RocketWeaponBehavior : IWeaponBehavior
    {
        ShootInteractor shootInteractor;
        Transform shootPoint;

        public RocketWeaponBehavior(ShootInteractor _interactor)
        {
            shootInteractor = _interactor;
            shootPoint = _interactor.GetShootPoint();

            shootInteractor.GetWeaponRenderer().material.color = Color.purple;
        }

        public void FireWeapon()
        {
            // - Get a pooled rocket from the object pool
            PooledObject pooledRocket = ObjectPool.Instance.GetPooledObject();
            pooledRocket.gameObject.SetActive(true);

            // - Get the Rigidbody and set the position and rotation of the bullet
            Rigidbody bullet = pooledRocket.GetComponent<Rigidbody>();
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = shootPoint.rotation;

            // - Apply a force to the bullet
            bullet.linearVelocity = shootPoint.forward * shootInteractor.GetShootVelocity();

            // - Recycle the bullet with object pool
            pooledRocket.DestroyWithTime(2f);
        }
    }
}
