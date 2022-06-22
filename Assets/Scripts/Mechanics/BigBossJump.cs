using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    internal class BigBossJump : Event<BigBossJump>
    {
        internal ShortcutZone shortcutZone;
        private BigBossController bigBoss;

        public override void Execute()
        {
            bigBoss = shortcutZone.bigBoss;
            bigBoss.ScaryJump();
        }
    }
}
