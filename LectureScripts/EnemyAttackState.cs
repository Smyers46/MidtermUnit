using UnityEngine;
using System.Collections;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemyController enemyController) : base(enemyController)
    {
        _controller = enemyController;
    }

    public override void OnStateEntered()
    {
        Debug.Log("Enemy has entered Attack State");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy has exited Attack State");
    }

    public override void OnStateUpdate()
    {
        Debug.Log("Attack Update...");

        // - Read distance to player
        float distance = Vector3.Distance(_controller.transform.position, _controller.target.transform.position);
        _controller.gameObject.transform.LookAt(_controller.target.transform.position);

        // - Go back to follow state if player is out of attack range
        if (distance > _controller.attackRange)
        {
            _controller.ChangeState(new EnemyFollowState(_controller));
        }


        if (_controller.canAttack)
        {
            GameObject bullet = GameObject.Instantiate(_controller.projectilePrefab, _controller.projectileSpawnReference.transform.position, _controller.transform.rotation);
            _controller.StartCoroutine(AttackDelay());
        }
        
        

    }

    public IEnumerator AttackDelay()
    {
        _controller.canAttack = false;
        yield return new WaitForSeconds(2f);
        _controller.canAttack = true;
    }
}