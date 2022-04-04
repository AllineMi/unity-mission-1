using Platformer.Core;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    /// <summary> Fired when a Player collides with an Enemy. </summary>
    public class PlayerEnemyCollision : Event<PlayerEnemyCollision>
    {
        internal EnemyController enemy;
        internal PlayerController player;

        public override void Execute()
        {
            var willHurtEnemy = player.Bounds.center.y >= enemy.BoundsEnemy.max.y;

            if (willHurtEnemy)
            {
                EnemyHurt();
            }
            else
            {
                PlayerHurt();
            }
        }

        private void EnemyHurt()
        {
            Simulation.Schedule<EnemyHurt>().enemy = enemy;
            player.Bounce(2);
        }

        private void PlayerHurt()
        {
            Simulation.Schedule<PlayerHurt>().player = player;
            Simulation.Schedule<EnemyHurtedPlayer>().enemy = enemy;
        }
    }
}
