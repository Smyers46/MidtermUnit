using UnityEngine;

namespace Midterm
{
    public abstract class FriendState
    {
        protected FriendController _controller;

        public FriendState(FriendController controller)
        {
            _controller = controller;
        }

        public abstract void OnStateEntered();

        public abstract void OnStateUpdate();

        public abstract void OnStateExit();


    }
}
