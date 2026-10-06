using UnityEngine;

namespace Midterm
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
            PooledObject pooledRocket = ObjectPool.Instance.GetPooledObject(true);
            if (pooledRocket != null)
            {
                pooledRocket.gameObject.SetActive(true);

                RocketScript rocketScript = pooledRocket.GetComponent<RocketScript>();
                rocketScript.Initialize(shootInteractor.gameObject.tag);

                // - Get the Rigidbody and set the position and rotation of the bullet
                Rigidbody rocket = pooledRocket.GetComponent<Rigidbody>();
                rocket.transform.position = shootPoint.position;
                rocket.transform.rotation = shootPoint.rotation;

                // - Apply a force to the bullet
                rocket.linearVelocity = shootPoint.forward * shootInteractor.GetShootVelocity();

                // - Recycle the bullet with object pool
                pooledRocket.DestroyWithTime(2f);
            }
            }
        }
    }
