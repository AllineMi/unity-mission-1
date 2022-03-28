using Platformer.Core;
using Platformer.Mechanics;

namespace Gameplay
{
    public class PlayerScared : Simulation.Event<PlayerScared>
    {
        public ShortcutZone shortcutZone;
        private PlayerController player;

        public override void Execute()
        {
            player = shortcutZone.player;
            player.jumpState = PlayerController.JumpState.PrepareToJump;
            player.scared = true;
            player.animator.SetTrigger("hurt");
        }
    }
}
