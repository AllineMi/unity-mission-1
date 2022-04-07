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

            player.PlayRunAnimation();
            friend.PlayRunAnimation();

            player.speed = 3f;
            friend.speed = 3f;

            player.MoveRight();
            friend.MoveRight();
        }
    }
}
