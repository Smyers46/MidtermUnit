using System.Net.NetworkInformation;
using UnityEngine;

namespace Midterm
{
    public class EnemyIdleState : EnemyState
    {
        public EnemyIdleState(EnemyController enemyController) : base(enemyController)
        {
            _controller = enemyController;
        }


        public override void OnStateEntered()
        {
            
        }

        public override void OnStateUpdate()
        {
            

            float distance = Vector3.Distance(_controller.transform.position, _controller.target.transform.position);
            Vector3 direction = (_controller.target.transform.position - _controller.transform.position).normalized;
            RaycastHit hit;

            // - Check if player / target is in range.
            if (distance <= _controller.detectionRange)
            {
                // - Run raycast to see if we see player
                if (Physics.Raycast(_controller.transform.position, direction, out hit, _controller.detectionRange))
                {
                    // - Check if the raycast hit the player directly
                    if (hit.collider.gameObject == _controller.target)
                    {
                        // - Only follow the player if the raycast sees the player
                        _controller.ChangeState(new EnemyFollowState(_controller));
                    }
                }
            }
        }

        public override void OnStateExit()
        {
            
        }
    }
}
