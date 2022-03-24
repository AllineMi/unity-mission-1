using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;

namespace Gameplay
{
    public class PlayerScared : Simulation.Event<PlayerScared>
    {
        public PlayerController player;

        public ShortcutZone shortcutZone;

        public override void Execute()
        {
            player = shortcutZone.player;
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

            player.scared = true;
            player.jumpTakeOffSpeed = 3;
            player.jumpState = PlayerController.JumpState.PrepareToJump;

            player.animator.SetTrigger("hurt");

            //rb.velocity = new Vector2(2f, 0f);


        }
    }
}
