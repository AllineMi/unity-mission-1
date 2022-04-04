using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;

namespace Platformer.Gameplay
{
    /// <summary> Used for Player respawn. When Player respawns, camera will follow Player to respawn point. </summary>
    public class CameraEnable : Simulation.Event<CameraEnable>
    {
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        internal PlayerController player;

        public override void Execute()
        {
            var playerTransform = player.transform;
            model.virtualCamera.m_Follow = playerTransform;
            model.virtualCamera.m_LookAt = playerTransform;
        }
    }
}
