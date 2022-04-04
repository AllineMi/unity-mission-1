using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    public class EnemyHurt : Simulation.Event<EnemyHurt>
    {
        internal EnemyController enemy;

        public override void Execute()
        {
            // Regular enemies. They don't have Health
            Simulation.Schedule<KillEnemy>().enemy = enemy;
        }
    }
}
