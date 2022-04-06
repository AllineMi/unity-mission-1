using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    public class FriendVictoryJump : Event<FriendVictoryJump>
    {
        public VictoryZone victoryZone;
        private FriendController friend;

        public override void Execute()
        {
            friend = victoryZone.friend;

            friend.PlayJumpAnimation();
            friend.speed = 1f;
            friend.MoveLeft();
        }
    }
}
