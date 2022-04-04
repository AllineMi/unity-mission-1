using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    public class EnemyHurtedPlayer : Simulation.Event<EnemyHurtedPlayer>
    {
        internal EnemyController enemy;

        public override void Execute()
        {
            enemy.StopMoving();
            Simulation.Schedule<EnemyStartMoving>(2f).enemy = enemy;
        }
    }
}
