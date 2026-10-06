using UnityEngine;

namespace Midterm
{
    public abstract class EnemyState
    {
        protected EnemyController _controller;

        public EnemyState(EnemyController controller)
        {
            _controller = controller;
        }

        public abstract void OnStateEntered();

        public abstract void OnStateUpdate();

        public abstract void OnStateExit();


    }
}
