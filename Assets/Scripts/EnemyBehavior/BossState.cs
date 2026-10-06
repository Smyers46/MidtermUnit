using UnityEngine;

namespace Midterm
{
    public abstract class BossState
    {
        protected BossEnemyController _controller;

        public BossState(BossEnemyController controller)
        {
            _controller = controller;
        }

        public abstract void OnStateEntered();

        public abstract void OnStateUpdate();

        public abstract void OnStateExit();

    }
}
