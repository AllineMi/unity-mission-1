using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;

        public AudioClip ouchAudio;
        public AudioSource audioSource;

        /// <summary> Initial jump velocity at the start of a jump. </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        public bool stopJump;
        public bool jump;

        public Collider2D collider2d;
        public Health health;

        /// <summary> Max horizontal speed of the player. </summary>
        public float maxSpeed = 7;

        public Vector2 move;
        public bool controlEnabled = true;
        public SpriteRenderer spriteRenderer;
        public Animator animator;

        public PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        //public int tokensCollected = 0;
        public Token token;
        public RuntimeAnimatorController playerControllerTokens;

        // SCARED
        internal bool scared;

        void Awake()
        {
            health = GetComponent<Health>();
            token = GetComponent<Token>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }

            if (scared && !controlEnabled && jumpState == JumpState.PrepareToJump)
            {
                JumpScare();
            }

            UpdateJumpState();
            base.Update();
        }

        private void JumpScare()
        {
            jumpTakeOffSpeed = 1f;
            animator.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0f);
            jump = false;
        }

        public void FlipPlayerX()
        {
            spriteRenderer.flipX = true;
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<CharacterJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }

                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }

                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    if (scared)
                    {
                        animator.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                        scared = false;
                        Schedule<EnablePlayerInput>(2f);
                        ResetPlayerTakeOffSpeed();
                    }

                    break;
            }
        }

        void ResetPlayerTakeOffSpeed()
        {
            jumpTakeOffSpeed = 7f;
        }

        private int frames = 0;

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            if (frames % 50 == 0)
            {
                Debug.Log($"PlayerController move.x: {move.x} #{gameObject.name}#");
                frames = 0;
            }
            else
            {
                frames++;
            }

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }

        // TODO to check
        public enum TokenSpriteType
        {
            SpriteNormal,
            SpriteToken10,
            SpriteToken20
        }
    }
}
