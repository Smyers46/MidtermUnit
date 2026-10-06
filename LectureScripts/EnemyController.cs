using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // - Enemy Properties
    public float moveSpeed = 3f;
    public float attackRange = 3f;
    public float detectionRange = 10f;
    public bool canAttack = true;

    public GameObject target = null;

    // - Combat
    public GameObject projectilePrefab = null;
    public GameObject projectileSpawnReference = null;

    // - Enemy States
    EnemyState currentState = null;

    private void Start()
    {
        ChangeState(new EnemyIdleState(this));        
    }

    private void Update()
    {
        if (currentState != null) 
            currentState.OnStateUpdate();
                        
    }

    public void ChangeState(EnemyState newState)
    {
        // - Call the current state's OnStateExit method before changing state.
        if (currentState != null) 
            currentState.OnStateExit();

        // - Set the new state
        currentState = newState;

        // - Call the new state's OnStateEntered method
        currentState.OnStateEntered();        
    }    

}
