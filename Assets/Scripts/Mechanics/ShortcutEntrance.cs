using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    public class ShortcutEntrance : BasePlayerColliderTrigger
    {
        private bool shortcutActivated;

        protected override void DoEnterTriggerAction()
        {
            if (shortcutActivated) return;

            player.DisableInput();
            player.DisableCollider();
            player.FlipPlayerToFaceWest();

            shortcutActivated = true;

            var epc = Schedule<EnablePlayerCollider>(1f);
            epc.player = player;
        }

        protected override void DoExitTriggerAction()
        {
            //throw new System.NotImplementedException();
        }
    }
}
