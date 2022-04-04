using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    public class EnemyStartMoving : Simulation.Event<EnemyStartMoving>
    {
        internal EnemyController enemy;

        public override void Execute()
        {
            enemy.StartMoving();
        }
    }
}
