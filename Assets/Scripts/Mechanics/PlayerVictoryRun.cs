using Platformer.Core;

namespace Platformer.Mechanics
{
    public class PlayerVictoryRun : Simulation.Event<PlayerVictoryRun>
    {
        public VictoryZone victoryZone;
        public PlayerController player;
        private FriendController friend;

        public override void Execute()
        {
            player = victoryZone.player;
            friend = victoryZone.friend;

            player.PlayVictoryRunAnimation();
            player.maxSpeed = 3f;
            player.MoveRight();

            friend.PlayVictoryRunAnimation();
            friend.speed = 3f;
            friend.MoveRight();
        }
    }
}
