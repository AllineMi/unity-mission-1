using Platformer.Core;

namespace Platformer.Mechanics
{
    public class PlayerVictory : Simulation.Event<PlayerVictory>
    {
        public VictoryZone victoryZone;
        PlayerController player;
        FriendController friend;

        public override void Execute()
        {
            player = victoryZone.player;
            friend = victoryZone.friend;

            player.PlayVictoryAnimation();
            friend.PlayVictoryAnimation();
        }
    }
}
