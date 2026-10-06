using UnityEngine;

namespace Midterm
{
    public class EnemyFollowState : EnemyState
    {
        public EnemyFollowState(EnemyController enemyController) : base(enemyController)
        {
            _controller = enemyController;
        }

        public override void OnStateEntered()
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.followState);
        }

        public override void OnStateUpdate()
        {
            // Read distance to player
            float distance = Vector3.Distance(
                _controller.transform.position,
                _controller.target.transform.position
            );

            // If player is in attack range, switch to attack state
            if (distance <= _controller.attackRange)
            {
                _controller.ChangeState(new EnemyAttackState(_controller));
                return;
            }

            // If the player is out of detection range, switch back to idle
            if (distance > _controller.detectionRange)
            {
                _controller.ChangeState(new EnemyIdleState(_controller));
                return;
            }

            // Run raycast to see if we see player
            Vector3 direction = (
                _controller.target.transform.position -
                _controller.transform.position
            ).normalized;

            RaycastHit hit;

            if (Physics.Raycast(
                _controller.transform.position,
                direction,
                out hit,
                _controller.detectionRange
            ))
            {
                // If something other than the player is blocking the ray
                if (hit.collider.gameObject != _controller.target)
                {
                    _controller.ChangeState(new EnemyIdleState(_controller));
                    return;
                }
            }

            // Move towards the player
            _controller.transform.position = Vector3.MoveTowards(
                _controller.transform.position,
                _controller.target.transform.position,
                _controller.moveSpeed * Time.deltaTime
            );

            // Face the player (withoutlooking up/down)
            Vector3 lookDirection = _controller.target.transform.position - _controller.transform.position;

            lookDirection.y = 0f;

            _controller.transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        public override void OnStateExit()
        {

        }
    }
}
