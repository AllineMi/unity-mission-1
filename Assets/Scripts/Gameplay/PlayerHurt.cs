using Gameplay;
using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary> Fired when the player gets hurt. </summary>
    public class PlayerHurt : Simulation.Event<PlayerHurt>
    {
        internal PlayerController player;

        public override void Execute()
        {
            player.playerHurt = true;
            player.DisableInput();
            player.health.Hurt();

            if (player.health.IsAlive)
            {
                Simulation.Schedule<HurtPlayer>().player = player;
            }
            else
            {
                Simulation.Schedule<KillPlayer>().player = player;
            }

            Simulation.Schedule<EnablePlayerInput>(1f).player = player;
            Simulation.Schedule<PlayerHurtFalse>(1f).player = player;

        }
    }
}
