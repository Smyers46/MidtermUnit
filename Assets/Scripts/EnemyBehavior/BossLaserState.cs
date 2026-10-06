using UnityEngine;

namespace Midterm
{
    public class BossLaserState : BossState
    {
        public BossLaserState(BossEnemyController controller)
            : base(controller)
        {
        }

        public override void OnStateEntered()
        {
            Debug.Log("Boss has entered laser attack state");

            _controller.laserAttack.StartLaserAttack(_controller.target.transform);
        }

        public override void OnStateUpdate()
        {
            // We'll handle this once LaserAttack can tell us
            // when its attack has finished.
        }

        public override void OnStateExit()
        {
            Debug.Log("Boss has left laser attack state");
        }
    }
}
