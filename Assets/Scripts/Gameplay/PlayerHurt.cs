using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary> Fired when the player gets hurt. </summary>
    public class PlayerHurt : Simulation.Event<PlayerHurt>
    {
        internal PlayerController player;

        public override void Execute()
        {
            player.health.Hurt();

            if (player.health.IsAlive)
            {
                Simulation.Schedule<HurtPlayer>().player = player;
            }
            else
            {
                Simulation.Schedule<KillPlayer>().player = player;
            }
        }
    }
}
