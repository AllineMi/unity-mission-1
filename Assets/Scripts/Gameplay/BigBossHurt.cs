using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    public class BigBossHurt : Simulation.Event<BigBossHurt>
    {
        internal BigBossController bigBoss;

        public override void Execute()
        {
            var enemyHealth = bigBoss.GetComponent<Health>();
            enemyHealth.Decrement();

            // bigBoss.health.Hurt();
            //
            // if (bigBoss.health.IsAlive)
            // {
            //     Simulation.Schedule<HurtBigBoss>().bigBoss = bigBoss;
            // }
            // else
            // {
            //     Simulation.Schedule<KillBigBoss>().bigBoss = bigBoss;
            // }
        }
    }
}
