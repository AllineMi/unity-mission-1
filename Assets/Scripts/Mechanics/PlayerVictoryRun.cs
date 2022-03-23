using Platformer.Core;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class PlayerVictoryRun : Simulation.Event<PlayerVictoryRun>
    {
        public VictoryZone victoryZone;
        public PlayerController player;
        private FriendController friend;

        public override void Execute()
        {
            /* For some reason, we are unable to get player from the victoryZone after we run
            FriendVictoryJump forcing us to pass it manually when schedulling this event.
            There must be away around, but I will not look at it now. */
            // If we don't get the player null error, we must get it from victoryZone
            if (victoryZone.player != null) player = victoryZone.player;
            friend = victoryZone.friend;

            player.animator.SetTrigger("victoryRun");
            Rigidbody2D playerRigidBody = player.animator.GetComponent<Rigidbody2D>();
            playerRigidBody.velocity = new Vector2(2f, 0f);

            friend.animator.SetTrigger("victoryRun");
            Rigidbody2D friendRigidBody = friend.animator.GetComponent<Rigidbody2D>();
            friendRigidBody.velocity = new Vector2(2f, 0f);
        }
    }
}
