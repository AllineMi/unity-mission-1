using static Platformer.Core.Simulation;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class BigBossJump : Event<BigBossJump>
    {
        public ShortcutZone shortcutZone;
        private BigBossController bigBoss;
        private Rigidbody2D bigBossRigidBody;
        public float bigBossJumpVelocity;

        public override void Execute()
        {
            bigBoss = shortcutZone.bigBoss;
            bigBossRigidBody = bigBoss.animator.GetComponent<Rigidbody2D>();

            bigBossRigidBody.velocity = new Vector2(bigBossJumpVelocity, 0f);
            bigBoss.jumpState = BigBossController.JumpState.PrepareToJump;
            Debug.Log($"BigBossJump: Execute");
        }
    }
}
