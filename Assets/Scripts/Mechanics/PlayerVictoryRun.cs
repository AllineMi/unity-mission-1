using Platformer.Core;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class PlayerVictoryRun  : Simulation.Event<PlayerVictoryRun>
    {
        public VictoryZone victoryZone;
        private PlayerController player;
        private PlayerController friend;

        public override void Execute()
        {
            player = victoryZone.player;
            friend = victoryZone.friend;
            friend.spriteRenderer.flipX = false;
            VictoryRun();
            friend.animator.SetTrigger("victoryRun");
            player.animator.SetTrigger("victoryRun");
        }

        private void VictoryRun()
        {
            Rigidbody2D playerRigidBody = player.animator.GetComponent<Rigidbody2D>();
            Rigidbody2D friendRigidBody = friend.animator.GetComponent<Rigidbody2D>();
            playerRigidBody.velocity = new Vector2(2f, 0f);
            friendRigidBody.velocity = new Vector2(2f, 0f);

        }
    }
}
