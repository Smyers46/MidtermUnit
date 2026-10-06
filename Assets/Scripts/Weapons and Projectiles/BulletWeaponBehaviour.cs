using UnityEngine;

namespace Midterm
{
    public class BulletWeaponBehavior : IWeaponBehavior
    {
        ShootInteractor shootInteractor;
        Transform shootPoint;

        public BulletWeaponBehavior(ShootInteractor _interactor)
        {
            shootInteractor = _interactor;
            shootPoint = _interactor.GetShootPoint();

            shootInteractor.GetWeaponRenderer().material.color = Color.green;
        }

        public void FireWeapon()
        {
            // - Get a pooled bullet from the object pool
            PooledObject pooledBullet = ObjectPool.Instance.GetPooledObject(false);
            if (pooledBullet != null)
            {

                pooledBullet.gameObject.SetActive(true);

                BulletScript bulletScript = pooledBullet.GetComponent<BulletScript>();
                bulletScript.Initialize(shootInteractor.gameObject.tag);

                // - Get the Rigidbody and set the position and rotation of the bullet
                Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
                bullet.transform.position = shootPoint.position;
                bullet.transform.rotation = shootPoint.rotation;

                // - Apply a force to the bullet
                bullet.linearVelocity = shootPoint.forward * shootInteractor.GetShootVelocity();

                // - Recycle the bullet with object pool
                pooledBullet.DestroyWithTime(2f);
            }
        }
    }
}
