using UnityEngine;

public class EnemyFollowState : EnemyState
{
    public EnemyFollowState(EnemyController enemyController) : base(enemyController)
    {
        _controller = enemyController;
    }

    public override void OnStateEntered()
    {
        Debug.Log("Enemy has entered Follow State");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy has exited Follow State");
    }

    public override void OnStateUpdate()
    {
        Debug.Log("Follow Update...");

        // - Move towards the player
        _controller.gameObject.transform.position = Vector3.MoveTowards(_controller.gameObject.transform.position, _controller.target.transform.position, _controller.moveSpeed * Time.deltaTime);
        _controller.gameObject.transform.LookAt(_controller.target.transform.position);
       
        // - Run raycast to see if we see player
        float distance = Vector3.Distance(_controller.transform.position, _controller.target.transform.position);
        Vector3 direction = (_controller.target.transform.position - _controller.transform.position).normalized;
        RaycastHit hit;
                
        // - A raycast for if the player hides behind a wall/object
        if (Physics.Raycast(_controller.transform.position, direction, out hit, _controller.detectionRange))
        {
            // - Check if the raycast did not the player directly
            if (hit.collider.gameObject != _controller.target)
            {
                // - Only follow the player if the raycast sees the player
                _controller.ChangeState(new EnemyIdleState(_controller));
            }
        }

        // - If player is in range to attack, switch to attack
        if (distance <= _controller.attackRange)
        {
            _controller.ChangeState(new EnemyAttackState(_controller));
        }

        // - If the player is out of detection range, switch back to idle
        if (distance > _controller.detectionRange)
        {
            _controller.ChangeState(new EnemyIdleState(_controller));
        }
    }
}