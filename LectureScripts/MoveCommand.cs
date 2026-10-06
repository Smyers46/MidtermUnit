using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : Command
{
    private NavMeshAgent agent;
    private Vector3 destination;

    // - Constructor for creating the command with values
    public MoveCommand(NavMeshAgent _agent, Vector3 _destination)
    {
       agent = _agent;
       destination = _destination;
    }

    // - Set the destination of the NavMeshAgent to the destination value for movement
    public override void Execute()
    {        
        agent.SetDestination(destination);
    }

    // - Use a lambda expression to check if the NavMeshAgent has reached the destination
    public override bool IsComplete => ReachedDestination();

    // - Check if the NavMeshAgent has reached the destination
    bool ReachedDestination()
    {
        if (agent.remainingDistance > 0.1f) 
            return false;

        return true;
    }
}
