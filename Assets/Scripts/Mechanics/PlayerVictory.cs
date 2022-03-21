using Platformer.Core;

namespace Platformer.Mechanics
{
    public class PlayerVictory  : Simulation.Event<PlayerVictory>
    {
        public VictoryZone victoryZone;
        private PlayerController player;
        private PlayerController friend;

        public override void Execute()
        {
            player = victoryZone.player;
            friend = victoryZone.friend;
            friend.animator.SetTrigger("victory");
            player.animator.SetTrigger("victory");
        }
    }
}