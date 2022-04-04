using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    public class HurtPlayer : Simulation.Event<HurtPlayer>
    {
        internal PlayerController player;

        public override void Execute()
        {
            player.DisableInput();
            player.PlayHurtAnimation();

            if (player.audioSourcePlayer && player.ouchAudioPlayer)
            {
                player.PlayHurtAudio();
            }

            // When the player gets hurt, it will move away from the enemy.
            if (player.spriteRendererPlayer.flipX) // Player facing West
            {
                player.JumpHurtRight();
            }
            else // Player facing East
            {
                player.JumpHurtLeft();
            }
        }
    }
}
