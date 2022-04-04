using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary> Fired when the player has died. </summary>
    public class KillPlayer : Simulation.Event<KillPlayer>
    {
        internal PlayerController player;

        public override void Execute()
        {
            Simulation.Schedule<CameraDisable>();

            player.DisableInput();

            if (player.audioSourcePlayer && player.ouchAudioPlayer)
            {
                player.PlayHurtAudio();
            }

            player.PlayHurtAnimation();
            player.PlayDeadAnimationActive(true);
            Simulation.Schedule<PlayerSpawn>(1);
        }
    }
}
