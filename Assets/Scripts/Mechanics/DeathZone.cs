using Platformer.Gameplay;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// DeathZone components mark a collider which will schedule a
    /// KillPlayer event when the player enters the trigger.
    /// </summary>
    public class DeathZone : BasePlayerColliderTrigger
    {
        protected override void DoEnterTriggerAction()
        {
            player.health.Die();
            Schedule<KillPlayer>().player = player;
        }

        protected override void DoExitTriggerAction()
        {
            // throw new System.NotImplementedException();
        }
    }
}
