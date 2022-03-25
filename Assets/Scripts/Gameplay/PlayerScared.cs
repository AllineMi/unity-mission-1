using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Gameplay
{
    public class PlayerScared : Simulation.Event<PlayerScared>
    {
        public ShortcutZone shortcutZone;
        public PlayerController player;

        public override void Execute()
        {
            player = shortcutZone.player;
            player.scared = true;
            player.jumpTakeOffSpeed = 3;
            player.jumpState = PlayerController.JumpState.PrepareToJump;

            player.animator.SetTrigger("hurt");

            // var model = Simulation.GetModel<PlatformerModel>();
            // var a = GameObject.FindGameObjectsWithTag("PlayerWaypoint");
            // Debug.Log($"PlayerWaypoint Name:  {a[0].name}");
        }
    }
}
