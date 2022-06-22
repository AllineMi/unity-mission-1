using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary> Fired when the player character lands after being airborne. </summary>
    public class PlayerLanded : Simulation.Event<PlayerLanded>
    {
        public BaseController baseController;

        public override void Execute()
        {
        }
    }
}
