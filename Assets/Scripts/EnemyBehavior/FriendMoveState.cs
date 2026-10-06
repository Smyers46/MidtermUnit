using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Midterm
{
    public class FriendMoveState : FriendState
    {
        private Vector3 destination;

        public FriendMoveState(FriendController friendController,Vector3 _destination) : base(friendController)
        {
            destination = _destination;
        }

        public override void OnStateEntered()
        {
            Debug.Log("Friend has entered move state");

            _controller.agent.isStopped = false;
            _controller.agent.stoppingDistance = 0f;
            _controller.agent.SetDestination(destination);
        }

        public override void OnStateUpdate()
        {
            // NavMeshAgent handles movement
        }

        public override void OnStateExit()
        {
            _controller.agent.isStopped = true;
            Debug.Log("Friend has left move state");
        }

    }
}
