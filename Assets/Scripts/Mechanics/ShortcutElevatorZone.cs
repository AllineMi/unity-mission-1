using UnityEngine;

namespace Platformer.Mechanics
{
    public class ShortcutElevatorZone : BasePlayerColliderTrigger
    {
        protected override void DoEnterTriggerAction()
        {
            Debug.Log($"Elevator Zone");
            player.DisableInput();
            player.MoveRight();
        }

        protected override void DoExitTriggerAction()
        {
            throw new System.NotImplementedException();
        }
    }
}
