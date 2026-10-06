using UnityEngine;
using System.Collections;

namespace Midterm
{
    public class EnemyAttackState : EnemyState
    {
        public EnemyAttackState(EnemyController enemyController) : base(enemyController)
        {
            _controller = enemyController;
        }

        public override void OnStateEntered()
        {
            Debug.Log("Enemy has entered attack state");
            AudioManager.Instance.PlaySFX(AudioManager.Instance.attackState);
        }

        public override void OnStateUpdate()
        {

            // Read distance to player
            float distance = Vector3.Distance(
                _controller.transform.position,
                _controller.target.transform.position
            );

            // Face the player (without looking up/down)
            Vector3 lookDirection = _controller.target.transform.position - _controller.transform.position;

            lookDirection.y = 0f;

            _controller.transform.rotation = Quaternion.LookRotation(lookDirection);

            // Go back to follow state if player is out of attack range
            if (distance > _controller.attackRange)
            {
                _controller.ChangeState(new EnemyFollowState(_controller));
                return;
            }

            // Attack
            if (_controller.canAttack)
            {
                // - Get a pooled bullet from the object pool
                PooledObject pooledBullet = ObjectPool.Instance.GetPooledObject(false);
                if (pooledBullet != null)
                {

                    pooledBullet.gameObject.SetActive(true);

                    BulletScript bulletScript = pooledBullet.GetComponent<BulletScript>();
                    bulletScript.Initialize(_controller.gameObject.tag);

                    // - Get the Rigidbody and set the position and rotation of the bullet
                    Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
                    bullet.transform.position = _controller.bulletSpawnReference.transform.position;
                    bullet.transform.rotation = _controller.transform.rotation;

                    // - Apply a force to the bullet
                    bullet.linearVelocity = _controller.transform.forward * 10f;

                    // - Recycle the bullet with object pool
                    pooledBullet.DestroyWithTime(2f);

                    Vector3 direction = (
                    _controller.target.transform.position -
                    _controller.bulletSpawnReference.transform.position
                ).normalized;

                    GameObject projectile = GameObject.Instantiate(
                        _controller.bulletPrefab,
                        _controller.bulletSpawnReference.transform.position,
                        Quaternion.LookRotation(direction)
                    );

                    _controller.StartCoroutine(AttackDelay());
                }

            }
        }

        public override void OnStateExit()
        {
            Debug.Log("Enemy has left attack state");
        }

        public IEnumerator AttackDelay()
        {
            _controller.canAttack = false;
            yield return new WaitForSeconds(2f);
            _controller.canAttack = true;
        }

    }
}
