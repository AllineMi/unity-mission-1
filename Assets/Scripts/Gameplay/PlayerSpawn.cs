using Platformer.Core;
using Platformer.Model;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary> Fired when the player is spawned after dying. </summary>
    public class PlayerSpawn : Simulation.Event<PlayerSpawn>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;

            player.DisableInput();

            if (player.audioSourcePlayer && player.respawnAudioPlayer)
            {
                player.PlayRespawnAudio();
            }

            player.health.Increment();
            player.Teleport(model.spawnPoint.transform.position);
            player.jumpStatePlayer = JumpStatePlayer.Grounded;
            player.PlayDeadAnimationActive(false);
            player.FlipPlayerToFaceWest();
            // Camera will follow player when respawned
            Simulation.Schedule<CameraEnable>().player = player;
            Simulation.Schedule<EnablePlayerInput>(2f);
        }
    }
}
