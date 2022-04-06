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
            friend.PlayRunAnimation();

            player.maxSpeed = 3f;
            friend.speed = 3f;

            player.MoveRight();
            friend.MoveRight();
        }
    }
}
