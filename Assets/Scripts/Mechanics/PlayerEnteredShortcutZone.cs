using Platformer.Mechanics;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    public class PlayerEnteredShortcutZone : Event<PlayerEnteredShortcutZone>
    {
        public ShortcutZone shortcutZone;
        public PlayerController player;
        public BigBossController bigBoss;

        public override void Execute()
        {
            // player = shortcutZone.player;
            // bigBoss = shortcutZone.bigBoss;
        }
    }
}
