using UnityEngine;
using UnityEngine.AI;

namespace Midterm
{
    public class MoveCommand : Command
    {
        private FriendController friend;
        private Vector3 destination;
        private GameObject pointer;
        private bool completed = false;
        

        // Constructor for creating the command with values
        public MoveCommand(FriendController _friend, Vector3 _destination, GameObject _pointer)
        {
            friend = _friend;
            destination = _destination;
            pointer = _pointer;
        }

        // Set the destination of the NavMeshAgent to the destination value for movement
        public override void Execute()
        {
            friend.MoveTo(destination);
        }

        // Using a lambda expression to check if the NavMeshAgent has reached the destination
        public override bool isComplete => ReachedDestination();

        // Check if NavMeshAgent has reached the destination
        private bool ReachedDestination() 
        {
            if (completed) return true;

            if (friend == null) 
            { 
                completed = true;
                return true; 
            }
            
            // Waiting for Unity to calculate the path
            if (friend.agent.pathPending) return false;
            
            // Still moving
            if (friend.agent.remainingDistance > 0.1f)
                return false;

            //Command is Complete
            Debug.Log("Move command completed");

            // Remove Pointer
            if (pointer != null)
            {
                Object.Destroy(pointer);
                pointer = null;
            }

            completed = true;

            // Return to following
            friend.StartFollowing();

            return true;
        }

    }
}
