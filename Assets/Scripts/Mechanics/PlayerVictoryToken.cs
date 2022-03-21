using Platformer.Core;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class PlayerVictoryToken : Simulation.Event<PlayerVictoryToken>
    {
        public VictoryZone victoryZone;
        private PlayerController player;
        private PlayerController friend;
        private Rigidbody2D playerRigidBody;
        private Rigidbody2D friendRigidBody;
        public override void Execute()
        {
            player = victoryZone.player;
            friend = victoryZone.friend;
            friend.spriteRenderer.flipX = false;
            player.jumpState = PlayerController.JumpState.Rolling;
            VictoryRun();
            friend.animator.SetTrigger("victoryRun");
            player.animator.SetTrigger("victoryRun");
        }

        private void VictoryRun()
        {
            playerRigidBody = player.animator.GetComponent<Rigidbody2D>();
            friendRigidBody = friend.animator.GetComponent<Rigidbody2D>();
            playerRigidBody.velocity = new Vector2(2f, 0f);
            friendRigidBody.velocity = new Vector2(2f, 0f);

        }
    }
}
