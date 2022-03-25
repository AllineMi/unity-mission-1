using UnityEngine;
using Platformer.Model;

namespace Platformer.Mechanics.Scripts
{
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class Jump : MonoBehaviour
    {
        float jumpVelocity;
        private bool jump;
        private bool stopJump;
        private bool scared;

        private bool IsGrounded;
        private PlatformerModel model;
        private Vector2 velocity;
        internal AnimationController control;


        public float jumpTakeOffSpeed;
        public float defaultJumpTakeOffSpeed;
        private Animator animator;
        private PlayerController player;
        private BigBossController bigBoss;
        private FriendController friend;

        private void Awake()
        {
             player = GetComponent<PlayerController>();
             bigBoss = GetComponent<BigBossController>();
             friend = GetComponent<FriendController>();
            control = GetComponent<AnimationController>();
            defaultJumpTakeOffSpeed = control.jumpTakeOffSpeed;
        }

        private void Update()
        {
            if (player != null && player.jumpState == PlayerController.JumpState.Landed)
            {
                animator.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                player.enabled = true;
                ResetPlayerTakeOffSpeed();
            }
        }

        public void StartJumping()
        {
            Rigidbody2D rb;
            if (player != null)
            {
                rb = player.GetComponent<Rigidbody2D>();
                //player.enabled = false;
                rb.velocity = new Vector2(jumpTakeOffSpeed, 0f);
                player.jumpState = PlayerController.JumpState.PrepareToJump;
            }
        }

        void ResetPlayerTakeOffSpeed()
        {
            player.jumpTakeOffSpeed = defaultJumpTakeOffSpeed;
        }
    }
}
