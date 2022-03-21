using Platformer.Core;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a Player collides with an Enemy.
    /// </summary>
    /// <typeparam name="EnemyCollision"></typeparam>
    public class PlayerEnemyCollision : Event<PlayerEnemyCollision>
    {
        public EnemyController enemy;
        public PlayerController player;

        public override void Execute()
        {
            var willHurtEnemy = player.Bounds.center.y >= enemy.Bounds.max.y;

            if (willHurtEnemy)
            {
                HurtEnemy();
            }
            else
            {
                HurtPlayer();
            }
        }

        private void HurtPlayer()
        {
            var playerHealth = player.health.currentHP;
            
            // if player health is above 0
            if (player.health.IsAlive)
            {
                Schedule<PlayerHurt>();
            }
            else
            {
                Schedule<PlayerDeath>();
            }
        }

        private void HurtEnemy()
        {
            var enemyHealth = enemy.GetComponent<Health>();
            
            if (enemyHealth != null)
            {
                enemyHealth.Decrement();
                if (!enemyHealth.IsAlive)
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                    player.Bounce(2);
                }
                // We don't want to bounce, since we will go change player x position 
                // else
                // {
                //     player.Bounce(7);
                // }
            }
            else
            {
                Schedule<EnemyDeath>().enemy = enemy;
                player.Bounce(2);
            }
        }
    }
}