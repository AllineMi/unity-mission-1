using Platformer.Core;
using Platformer.Mechanics;

namespace Gameplay
{
    public class PlayerScaredFalse : Simulation.Event<PlayerScaredFalse>
    {
        internal PlayerController player;

        public override void Execute()
        {
            player.playerScared = false;
        }
    }
}
