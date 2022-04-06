using Gameplay;
using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    internal class PlayerScared : Simulation.Event<PlayerScared>
    {
        internal ShortcutZone shortcutZone;
        private PlayerController player;

        public override void Execute()
        {
            player = shortcutZone.player;
            player.JumpScare();
            Simulation.Schedule<EnablePlayerInput>(1f).player = player;
            Simulation.Schedule<PlayerScaredFalse>(1f).player = player;
        }
    }
}
