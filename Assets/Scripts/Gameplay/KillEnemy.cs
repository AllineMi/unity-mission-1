using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary> Fired when the health component on an enemy has a hitpoint value of 0. </summary>
    public class KillEnemy : Simulation.Event<KillEnemy>
    {
        internal EnemyController enemy;

        public override void Execute()
        {
            enemy.colliderEnemy.enabled = false;

            if (enemy.audioeEnemy && enemy.ouch)
            {
                enemy.PlayHurtAudio();
            }
        }
    }
}
