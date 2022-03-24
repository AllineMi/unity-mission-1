using static Platformer.Core.Simulation;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class FriendVictoryJump : Event<FriendVictoryJump>
    {
        public VictoryZone victoryZone;
        private FriendController friend;
        private Rigidbody2D friendRigidBody;

        public override void Execute()
        {
            friend = victoryZone.friend;
            friendRigidBody = friend.animator.GetComponent<Rigidbody2D>();

            friendRigidBody.velocity = new Vector2(-0.8f, 0f);
            friend.jumpState = FriendController.JumpState.PrepareToJump;
            friend.spriteRenderer.flipX = false;
        }
    }
}
