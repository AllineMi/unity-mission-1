using UnityEngine;
using Platformer.Core;
using Platformer.Gameplay;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : BaseController
    {
        private float defaultValue;

        #region AUDIO

        [Header("AUDIO SETTINGS ----------------------")]
        public AudioClip jumpAudioPlayer;

        public AudioClip ouchAudioPlayer;
        public AudioClip respawnAudioPlayer;
        public AudioSource audioSourcePlayer;

        #endregion

        #region BOUNDS

        public Bounds Bounds => collider2dPlayer.bounds;

        #endregion

        #region COLLIDER

        public Collider2D collider2dPlayer;

        #endregion

        #region HEALTH

        [Header("HEALTH / TOKEN SETTINGS ----------------------")]
        public Health health;

        #endregion

        #region HURT

        [Header("HURT SETTINGS ----------------------")]
        public float hurtJumpTakeOffSpeedPlayer = 5f;

        public float hurtMaxSpeedPlayer = 2f;
        internal bool playerHurt;

        #endregion

        #region MOVEMENT

        [Header("MOVEMENT SETTINGS ----------------------")]
        public bool controlEnabledPlayer = true;

        #endregion

        #region SCARED

        internal bool playerScared;
        internal bool playerJumpScaredRan;

        #endregion

        #region SHORTCUT

        [Header("SHORTCUT SETTINGS ----------------------")]
        public bool playerStayOnElevator;

        #endregion

        #region TOKEN

        [Header("TOKEN SETTINGS ----------------------")]
        public Token token;

        public RuntimeAnimatorController playerControllerTokens;

        #endregion

        protected override void Awake()
        {
            // AUDIO
            audioSourcePlayer = GetComponent<AudioSource>();

            // COLLIDER
            collider2dPlayer = GetComponent<Collider2D>();

            // HEALTH
            health = GetComponent<Health>();

            // TOKEN
            token = GetComponent<Token>();

            base.Awake();
        }

        protected override void Update()
        {
            #region controlEnabledTrue

            if (controlEnabledPlayer)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpStatePlayer.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpStatePlayer.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Simulation.Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }

            #endregion

            #region controlEnabledFalse

            if (!controlEnabledPlayer)
            {
                if (jumpState == JumpStatePlayer.Landed)
                {
                    if (playerScared || playerHurt)
                    {
                        StopMoving();
                        ResetPlayerJumpTakeOffSpeed();
                        ResetSpeedPlayer();
                    }
                }

                switch (moveDirection)
                {
                    case MoveDirections.None: // This resets the velocity of the Rigidbody
                        rigidBody.velocity = resetVector;
                        break;

                    case MoveDirections.Left: // This moves the Rigidbody to the left (minus right Vector)
                        rigidBody.velocity = -rightVector * maxSpeed;
                        break;

                    case MoveDirections.Right: // This moves the Rigidbody to the right
                        rigidBody.velocity = rightVector * maxSpeed;
                        velocity.x = maxSpeed;
                        break;

                    case MoveDirections.Up: // Change the velocity so that the Rigidbody travels upwards
                        velocity.y = 1f;
                        break;

                    case MoveDirections.Down: // This moves the Rigidbody down
                        rigidBody.velocity = -upVector * maxSpeed;
                        break;
                }
            }

            #endregion

            base.Update();
        }

        #region ANIMATION

        internal void PlayDeadAnimationActive(bool trueOrFalse)
        {
            animator.SetBool("dead", trueOrFalse);
        }

        internal void BecomeBigger()
        {
            animator.runtimeAnimatorController = playerControllerTokens;
        }

        #endregion

        #region AUDIO

        internal void PlayHurtAudio()
        {
            audioSourcePlayer.PlayOneShot(ouchAudioPlayer);
        }

        internal void PlayRespawnAudio()
        {
            audioSourcePlayer.PlayOneShot(respawnAudioPlayer);
        }

        #endregion

        #region COLLIDER

        internal void EnableCollider()
        {
            collider2dPlayer.enabled = true;
        }

        internal void DisableCollider()
        {
            collider2dPlayer.enabled = false;
        }

        #endregion

        #region HURT

        /// <summary> If Player facing East, if spriteRenderer Flip X False </summary>
        internal void JumpHurtLeft()
        {
            maxSpeed = hurtMaxSpeedPlayer;
            Jump(hurtJumpTakeOffSpeedPlayer);
            MoveLeft();
        }

        /// <summary> If Player facing West, if spriteRenderer Flip X True </summary>
        internal void JumpHurtRight()
        {
            maxSpeed = hurtMaxSpeedPlayer;
            Jump(hurtJumpTakeOffSpeedPlayer);
            MoveRight();
        }

        #endregion

        #region JUMP

        void Jump(float characterJumpVelocity)
        {
            defaultValue = jumpTakeOffSpeed;
            jumpTakeOffSpeed = characterJumpVelocity;
            jumpState = JumpStatePlayer.PrepareToJump;
        }

        public override void Jump()
        {
            Jump(jumpTakeOffSpeed);
        }

        void ResetPlayerJumpTakeOffSpeed()
        {
            jumpTakeOffSpeed = defaultValue;
            defaultValue = 0;
        }

        #endregion

        #region MOVEMENT

        protected override void ComputeVelocity()
        {
            base.ComputeVelocity();

            if (!controlEnabledPlayer)
            {
                if (playerHurt || playerScared)
                {
                    if (rigidBody.velocity.x < -0.01f)
                    {
                        FlipPlayerToFaceEast();
                    }

                    if (rigidBody.velocity.x > 0.01f)
                    {
                        FlipPlayerToFaceWest();
                    }
                }
            }
        }

        void ResetSpeedPlayer()
        {
            maxSpeed = 5f;
        }

        internal void EnableInput()
        {
            controlEnabledPlayer = true;
        }

        internal void DisableInput()
        {
            controlEnabledPlayer = false;
        }

        #endregion

        #region SCARED

        internal void JumpScare()
        {
            playerScared = true;
            playerJumpScaredRan = true;
            animator.SetTrigger("hurt");
            jumpState = JumpStatePlayer.PrepareToJump;
            MoveRight();
            jumpTakeOffSpeed = 1f;
        }

        #endregion
    }
}
