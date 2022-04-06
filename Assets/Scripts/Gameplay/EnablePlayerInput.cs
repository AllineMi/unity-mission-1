using Platformer.Core;
using Platformer.Mechanics;

namespace Gameplay
{
    /// <summary> This event is fired when user input should be enabled. </summary>
    public class EnablePlayerInput : Simulation.Event<EnablePlayerInput>
    {
        internal PlayerController player;

        public override void Execute()
        {
            player.EnableInput();
        }
    }
}
