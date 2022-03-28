using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;

namespace Gameplay
{
    public class StartRunning : Simulation.Event<StartRunning>
    {
        public PlayerController player;
        public BigBossController bigBoss;
        public ShortcutZone shortcutZone;

        public override void Execute()
        {
            player = shortcutZone.player;
            bigBoss = shortcutZone.bigBoss;
            player.spriteRenderer.flipX = false;

            // TODO use playerRigidBody.rotation = 50f; to make seem that is running away
            //player.GetComponent<Rigidbody2D>().rotation = 50f;
            Rigidbody2D playerRigidBody = player.animator.GetComponent<Rigidbody2D>();
            playerRigidBody.velocity = new Vector2(2f, 0f);

            //bigBoss.velocity = new Vector2(2f, 0f);
        }
    }
}
