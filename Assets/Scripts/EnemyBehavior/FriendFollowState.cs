using UnityEngine;

namespace Midterm
{
    public class FriendFollowState : FriendState
    {
        public FriendFollowState(FriendController friendController) : base(friendController)
        {
            _controller = friendController;
        }

        public override void OnStateEntered()
        {
            Debug.Log("Friend has entered follow state");
            AudioManager.Instance.PlaySFX(AudioManager.Instance.greeting);
        }

        public override void OnStateUpdate()
        {
            // Read distance to player
            float distance = Vector3.Distance(
                _controller.transform.position,
                _controller.target.transform.position
            );
            // Stop when close enough to the player
            if (distance > _controller.stoppingDistance)
            {
                _controller.transform.position = 
                Vector3.MoveTowards(_controller.transform.position,_controller.target.transform.position,_controller.moveSpeed * Time.deltaTime);
            }

            // Face the player (withoutlooking up/down)
            Vector3 lookDirection = _controller.target.transform.position - _controller.transform.position;

            lookDirection.y = 0f;

            if (lookDirection != Vector3.zero)
            {
                _controller.transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }

        public override void OnStateExit()
        {
            Debug.Log("Friend has left follow state");
        }
    }
}
