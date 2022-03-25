using static Platformer.Core.Simulation;
using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Collider2D))]
    public class BigBossController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioSource audioSource;
        public Vector3 defaultPosition = new Vector3(-10.6068935f, -12.643858f, -0.401916862f);

        /// <summary> Initial jump velocity at the start of a jump. </summary>
        public float jumpTakeOffSpeed = 5;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        public bool jump;

        public Collider2D collider2d;

        public float maxSpeed = 1;

        Vector2 move;
        public SpriteRenderer spriteRenderer;
        private Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;
        public bool canBecomeBigger;
        private float timer = 0.0f;
        private float duration = 20f;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            if (jump)
            {
                velocity = new Vector2(2f, 0f);
                jump = false;
            }

            if (canBecomeBigger)
            {
                BecomeBigger();
            }

            UpdateJumpState();
            base.Update();
        }

        public void StopMoving()
        {
            velocity = new Vector2(0f, 0f);
        }

        private void BecomeBigger()
        {
            Transform transformScalePosition = transform;

            var finalPosition = new Vector3(-6.106885f, -10.90f, -0.4019169f);
            var finalScale = new Vector3(6, 6, 6);

            if (transformScalePosition.localScale.y < 6f)
            {
                timer += Time.deltaTime;
                float fractionOfJourney = timer / duration;

                // For Position
                transformScalePosition.position = Vector3.Lerp(transformScalePosition.position, finalPosition,
                    fractionOfJourney);

                // For Scale
                transformScalePosition.localScale = Vector3.Lerp(transformScalePosition.localScale, finalScale,
                    fractionOfJourney);
            }
            else
            {
                canBecomeBigger = false;
                timer = 0.0f;
            }
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
                        Schedule<PlayerJumped>().bigBoss = this;
                        jumpState = JumpState.InFlight;
                    }

                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().bigBoss = this;
                        jumpState = JumpState.Landed;
                    }

                    break;
                case JumpState.Landed:
                    animator.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }

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
            //
            // animator.SetBool("grounded", IsGrounded);
            // animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }
    }
}
