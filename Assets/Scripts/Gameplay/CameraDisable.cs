using Platformer.Core;
using Platformer.Model;

namespace Platformer.Gameplay
{
    /// <summary> Used for Player respawn. </summary>
    public class CameraDisable : Simulation.Event<CameraDisable>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            model.virtualCamera.m_Follow = null;
            model.virtualCamera.m_LookAt = null;
        }
    }
}
