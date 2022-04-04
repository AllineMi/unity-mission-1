using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    public class PlayerBecomeBigger : Simulation.Event<PlayerBecomeBigger>
    {
        public PlayerController player;

        public override void Execute()
        {
            player.BecomeBigger();
        }
    }
}
