using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Midterm
{
    public class BossFollowState : BossState
    {
        public BossFollowState(BossEnemyController controller)
                    : base(controller)
        {
        }

        public override void OnStateEntered()
        {
            Debug.Log("Boss has entered follow state");
        }

        public override void OnStateUpdate()
        {
            if (_controller.target == null)
                return;

            float distance = Vector3.Distance(_controller.transform.position, _controller.target.transform.position);

            // Enter laser attack range
            if (distance <= _controller.attackRange)
            {
                _controller.ChangeState(new BossLaserState(_controller));

                return;
            }

            // Move toward player
            Vector3 direction =_controller.target.transform.position - _controller.transform.position;

            direction.y = 0f;

            direction.Normalize();

            _controller.transform.position += direction * _controller.moveSpeed * Time.deltaTime;

            // Face player
            if (direction != Vector3.zero)
            {
                _controller.transform.rotation = Quaternion.LookRotation(direction);
            }
        }

        public override void OnStateExit()
        {
            Debug.Log("Boss has left follow state");
        }
    }
}
