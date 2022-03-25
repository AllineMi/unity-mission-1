using Platformer.Mechanics;
using static Platformer.Core.Simulation;

namespace Gameplay
{
    public class BigBossBigger : Event<BigBossBigger>
    {
        public ShortcutZone shortcutZone;
        private BigBossController bigBoss;

        public override void Execute()
        {
            bigBoss = shortcutZone.bigBoss;
            bigBoss.StopMoving();
            bigBoss.canBecomeBigger = true;
        }
    }
}
