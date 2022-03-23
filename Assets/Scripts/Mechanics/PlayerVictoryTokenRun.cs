using Platformer.Core;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class PlayerVictoryTokenRun : Simulation.Event<PlayerVictoryTokenRun>
    {
        public VictoryZone victoryZone;

        private PlayerController player;
        private Rigidbody2D playerRigidBody;

        private FriendController friend;
        private Rigidbody2D friendRigidBody;

        public override void Execute()
        {
            player = victoryZone.player;
            if (player == null)Debug.Log($"PlayerVictoryTokenRun: player null");
            playerRigidBody = player.animator.GetComponent<Rigidbody2D>();
            if (playerRigidBody == null)Debug.Log($"PlayerVictoryTokenRun: playerRigidBody null");

            friend = victoryZone.friend;
            if (friend == null)Debug.Log($"PlayerVictoryTokenRun: friend null");
            friendRigidBody = friend.animator.GetComponent<Rigidbody2D>();
            if (friendRigidBody == null)Debug.Log($"PlayerVictoryTokenRun: friendRigidBody null");

            FriendJump();
            var pvr = Schedule<DetectFriendLanded>();
            VictoryTokenRun();
        }

        private void FriendJump()
        {
            friend.jumpState = FriendController.JumpState.PrepareToJump;

            // if (friend.jumpState == FriendController.JumpState.Grounded) friendRigidBody.velocity = new Vector2(0f, 0f);
        }

        private void VictoryTokenRun()
        {
            Debug.Log($"PlayerVictoryTokenRun: entered VictoryTokenRun");
            if(friendRigidBody == null) Debug.Log($"PlayerVictoryTokenRun: friendRigidBody null");
            Debug.Log($"PlayerVictoryTokenRun: friend.jumpState {friend.jumpState}");
            if (friend.jumpState == FriendController.JumpState.Grounded)
            {
                friendRigidBody.velocity = new Vector2(-1f, 0f);
                friend.spriteRenderer.flipX = false;
                friend.animator.SetTrigger("victoryRun");
                player.animator.SetTrigger("victoryRun");

                //playerRigidBody.velocity = new Vector2(2f, 0f);
                friendRigidBody.velocity = new Vector2(2f, 0f);
            }

        }
    }

    public class DetectFriendLanded : Event<DetectFriendLanded>
    {
        public override void Execute()
        {
            player = victoryZone.player;
            friend = victoryZone.friend;
            if (player.token.currentToken >= 20) colletedAllTokens = true;
            player.controlEnabled = false;
            model.virtualCamera.enabled = false;

            if (friend == null) Debug.Log($"PlayerEnteredVictoryZone: Friend is NULL");
            if (player == null) Debug.Log($"PlayerEnteredVictoryZone: Player is NULL");
            CheckVictoryCondition();
            Celebrate();
        }
    }
}
