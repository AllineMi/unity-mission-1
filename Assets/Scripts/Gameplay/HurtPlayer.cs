using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class HurtPlayer : Simulation.Event<HurtPlayer>
    {
        internal PlayerController player;

        public override void Execute()
        {
            player.PlayHurtAnimation();

            if (player.audioSourcePlayer && player.ouchAudioPlayer)
            {
                player.PlayHurtAudio();
            }

            // When the player gets hurt, it will move away from the enemy.
            if (!player.spriteRendererPlayer.flipX) // Player facing East
            {
                player.JumpHurtLeft();
            }
            else // Player facing West
            {
                player.JumpHurtRight();
            }
        }
    }
}
