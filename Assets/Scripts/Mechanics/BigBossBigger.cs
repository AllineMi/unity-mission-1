using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    internal class BigBossBigger : Event<BigBossBigger>
    {
        internal ShortcutZone shortcutZone;
        private BigBossController bigBoss;

        public override void Execute()
        {
            bigBoss = shortcutZone.bigBoss;
            bigBoss.canBecomeBigger = true;
        }
    }
}
