using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary> Fired when a player collides with a token. </summary>
    public class PlayerTokenCollision : Simulation.Event<PlayerTokenCollision>
    {
        internal PlayerController player;
        public TokenInstance tokenInstance;

        public override void Execute()
        {
            tokenInstance.PlayCollectAudio();

            player.health.Increment();
            player.token.Increment();

            if (player.token.allTokensCollected)
            {
                Simulation.Schedule<PlayerBecomeBigger>().player = player;
            }
        }
    }
}
