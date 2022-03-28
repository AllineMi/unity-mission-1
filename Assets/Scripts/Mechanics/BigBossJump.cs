using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    public class BigBossJump : Event<BigBossJump>
    {
        public ShortcutZone shortcutZone;
        private BigBossController bigBoss;
        private float bigBossJumpVelocity = 5f;

        public override void Execute()
        {
            bigBoss = shortcutZone.bigBoss;
            bigBoss.Jump(bigBossJumpVelocity);
        }
    }
}
