using Platformer.Gameplay;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary> Marks a trigger as a VictoryZone, usually used to end the current game level. </summary>
    public class VictoryZone : BasePlayerColliderTrigger
    {
        public FriendController friend;

        protected override void DoEnterTriggerAction()
        {
            var ev = Schedule<PlayerEnteredVictoryZone>();
            ev.victoryZone = this;
        }

        protected override void DoExitTriggerAction()
        {
            // throw new System.NotImplementedException();
        }
    }
}
