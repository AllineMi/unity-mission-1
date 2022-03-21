using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class PlayerRolling : Simulation.Event<PlayerRolling>
    {
        public PlayerController player;
        private Rigidbody2D playerRigidBody;
        private const float toRotate = 10f;

        public override void Execute()
        {
            Debug.Log($"oi");
            //player.transform.Rotate(0f,0f,toRotate);
            //player.transform.Rotate(0f,0f,toRotate);
            playerRigidBody.rotation = toRotate;
        }
    }
}
