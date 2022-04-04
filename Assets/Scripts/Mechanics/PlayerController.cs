using UnityEngine;
using Platformer.Core;
using Platformer.Model;
using Platformer.Gameplay;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public Bounds Bounds => collider2dPlayer.bounds;
        readonly PlatformerModel modelPlayer = Simulation.GetModel<PlatformerModel>();

        #region ANIMATION VARIABLES

        // ANIMATION VARIABLES
        internal Animator animatorPlayer;
        internal SpriteRenderer spriteRendererPlayer;

        #endregion

        #region AUDIO VARIABLES

        // AUDIO VARIABLES
        [Header("AUDIO SETTINGS ----------------------")]
        public AudioClip jumpAudioPlayer;

        public AudioClip ouchAudioPlayer;
        public AudioClip respawnAudioPlayer;
        public AudioSource audioSourcePlayer;

        #endregion

        #region COLLIDER VARIABLES

        public Collider2D collider2dPlayer;

        #endregion

        #region HURT VARIABLES

        // HURT VARIABLES
        [Header("HURT SETTINGS ----------------------")]
        public float hurtJumpTakeOffSpeedPlayer = 5f;

        public float hurtMaxSpeedPlayer = 2f;
        bool playerHurt;

        #endregion

        #region JUMP VARIABLES

        // JUMP VARIABLES
        [Header("JUMP SETTINGS ----------------------")]
        public JumpStatePlayer jumpStatePlayer = JumpStatePlayer.Grounded;

        public float jumpTakeOffSpeedPlayer = 5;
        bool jumpPlayer;
        bool stopJumpPlayer;

        #endregion

        #region MOVEMENT VARIABLES

        // MOVEMENT VARIABLES
        [Header("MOVEMENT SETTINGS ----------------------")]
        public bool controlEnabledPlayer = true;

        public float speed = 5;
        public float maxSpeed = 5;
        MoveDirectionPlayer moveDirectionPlayer;
        Rigidbody2D rigidbodyPlayer;
        Vector2 movePlayer;
        Vector3 resetVectorPlayer;
        Vector3 rightVectorPlayer;
        Vector3 upVectorPlayer;

        #endregion

        #region SCARED VARIABLES

        // SCARED VARIABLES
        internal bool playerScared;
        internal bool playerJumpScaredRan;

        #endregion

        #region SHORTCUT VARIABLES

        // SHORTCUT VARIABLES
        [Header("SHORTCUT SETTINGS ----------------------")]
        public bool playerStayOnElevator;

        #endregion

        #region HEALTH / TOKEN VARIABLES

        // HEALTH / TOKEN VARIABLES
        [Header("HEALTH / TOKEN SETTINGS ----------------------")]
        public Health health;

        public Token token;
        public RuntimeAnimatorController playerControllerTokens;

        #endregion

        void Awake()
        {
            // ANIMATION
            animatorPlayer = GetComponent<Animator>();
            spriteRendererPlayer = GetComponent<SpriteRenderer>();

            // AUDIO
            audioSourcePlayer = GetComponent<AudioSource>();

            // COLLIDER
            collider2dPlayer = GetComponent<Collider2D>();

            // MOVEMENT
            rigidbodyPlayer = GetComponent<Rigidbody2D>();
            //This starts with the Rigidbody not moving in any direction at all
            moveDirectionPlayer = MoveDirectionPlayer.None;
            //This Vector is set to 1 in the y axis (for moving upwards)
            upVectorPlayer = Vector3.up;
            //This Vector is set to 1 in the x axis (for moving in the right direction)
            rightVectorPlayer = Vector3.right;
            //This Vector is zeroed out for when the Rigidbody should not move
            resetVectorPlayer = Vector3.zero;

            // HEALTH / TOKEN
            health = GetComponent<Health>();
            token = GetComponent<Token>();
        }

        protected override void Update()
        {
            if (controlEnabledPlayer)
            {
                movePlayer.x = Input.GetAxis("Horizontal");
                if (jumpStatePlayer == JumpStatePlayer.Grounded && Input.GetButtonDown("Jump"))
                    jumpStatePlayer = JumpStatePlayer.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJumpPlayer = true;
                    Simulation.Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                movePlayer.x = 0;
            }

            if (!controlEnabledPlayer)
            {
                //This switches the direction depending on button presses
                switch (moveDirectionPlayer)
                {
                    //The starting state which resets the object
                    case MoveDirectionPlayer.None:
                        //This resets the velocity of the Rigidbody
                        rigidbodyPlayer.velocity = resetVectorPlayer;
                        //m_Rigidbody.velocity = new Vector2(0f,0f);
                        break;

                    //This is for moving in an upwards direction
                    case MoveDirectionPlayer.Up:
                        //Change the velocity so that the Rigidbody travels upwards
                        //m_Rigidbody.velocity = m_PlayerUpVector * speed;
                        velocity.y = 1f;
                        break;

                    //This is for moving left
                    case MoveDirectionPlayer.Left:
                        //This moves the Rigidbody to the left (minus right Vector)
                        rigidbodyPlayer.velocity = -rightVectorPlayer * maxSpeed;
                        break;

                    //This is for moving right
                    case MoveDirectionPlayer.Right:
                        //This moves the Rigidbody to the right
                        rigidbodyPlayer.velocity = rightVectorPlayer * maxSpeed;
                        velocity.x = maxSpeed;
                        break;

                    //This is for moving down
                    case MoveDirectionPlayer.Down:
                        //This moves the Rigidbody down
                        rigidbodyPlayer.velocity = -upVectorPlayer * maxSpeed;
                        break;
                }
            }

            UpdateJumpStatePlayer();
            base.Update();
        }

        // TODO TO CHECK
        public enum TokenSpriteType
        {
            SpriteNormal,
            SpriteToken10,
            SpriteToken20
        }

        #region ANIMATION

        internal void PlayDeadAnimationActive(bool trueOrFalse)
        {
            animatorPlayer.SetBool("dead", trueOrFalse);
        }

        internal void PlayVictoryRunAnimation()
        {
            animatorPlayer.SetTrigger("victoryRun");
        }

        internal void PlayVictoryAnimation()
        {
            animatorPlayer.SetTrigger("victory");
        }

        internal void PlayHurtAnimation()
        {
            animatorPlayer.SetTrigger("hurt");
        }

        /// <summary> SpriteRenderer flip X will be true </summary>
        public void FlipPlayerToFaceEast()
        {
            spriteRendererPlayer.flipX = true;
        }

        /// <summary> SpriteRenderer flip X will be false </summary>
        internal void FlipPlayerToFaceWest()
        {
            spriteRendererPlayer.flipX = false;
        }

        internal void BecomeBigger()
        {
            animatorPlayer.runtimeAnimatorController = playerControllerTokens;
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

        #region JUMP

        void UpdateJumpStatePlayer()
        {
            jumpPlayer = false;
            switch (jumpStatePlayer)
            {
                case JumpStatePlayer.PrepareToJump:
                    jumpStatePlayer = JumpStatePlayer.Jumping;
                    jumpPlayer = true;
                    stopJumpPlayer = false;
                    break;
                case JumpStatePlayer.Jumping:
                    if (!IsGrounded)
                    {
                        Simulation.Schedule<CharacterJumped>().player = this;
                        jumpStatePlayer = JumpStatePlayer.InFlight;
                    }

                    break;
                case JumpStatePlayer.InFlight:
                    if (IsGrounded)
                    {
                        Simulation.Schedule<PlayerLanded>().player = this;
                        jumpStatePlayer = JumpStatePlayer.Landed;
                    }

                    break;
                case JumpStatePlayer.Landed:
                    jumpStatePlayer = JumpStatePlayer.Grounded;
                    if (playerScared || playerHurt)
                    {
                        StopMoving();
                        //animatorPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                        playerScared = false;
                        Simulation.Schedule<EnablePlayerInput>(1f).player = this;
                        ResetPlayerJumpTakeOffSpeed();
                        ResetSpeedPlayer();
                    }

                    break;
            }
        }

        void ResetPlayerJumpTakeOffSpeed()
        {
            jumpTakeOffSpeedPlayer = 7f;
        }

        internal void JumpScare()
        {
            playerScared = true;
            playerJumpScaredRan = true;
            animatorPlayer.SetTrigger("hurt");
            jumpStatePlayer = JumpStatePlayer.PrepareToJump;
            MoveRight();
            jumpTakeOffSpeedPlayer = 1f;
        }

        internal void JumpHurtLeft()
        {
            playerHurt = true;
            maxSpeed = hurtMaxSpeedPlayer;
            jumpTakeOffSpeedPlayer = hurtJumpTakeOffSpeedPlayer;
            Jump();
            MoveLeft();
        }

        internal void JumpHurtRight()
        {
            playerHurt = true;
            maxSpeed = hurtMaxSpeedPlayer;
            jumpTakeOffSpeedPlayer = hurtJumpTakeOffSpeedPlayer;
            Jump();
            MoveRight();
        }

        void Jump(float characterJumpVelocity)
        {
            jumpStatePlayer = JumpStatePlayer.PrepareToJump;
        }

        void Jump()
        {
            Jump(jumpTakeOffSpeedPlayer);
        }

        #endregion

        #region MOVEMENT

        protected override void ComputeVelocity()
        {
            if (jumpPlayer && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeedPlayer * modelPlayer.jumpModifier;
                jumpPlayer = false;
            }
            else if (stopJumpPlayer)
            {
                stopJumpPlayer = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * modelPlayer.jumpDeceleration;
                }
            }

            if (movePlayer.x > 0.01f || rigidbodyPlayer.velocity.x > 0.01f)
                FlipPlayerToFaceWest();
            else if (movePlayer.x < -0.01f || rigidbodyPlayer.velocity.x > 0.01f)
                FlipPlayerToFaceEast();

            animatorPlayer.SetBool("grounded", IsGrounded);
            animatorPlayer.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = movePlayer * maxSpeed;
        }

        void ResetSpeedPlayer()
        {
            maxSpeed = 5f;
        }

        internal void MoveUp()
        {
            moveDirectionPlayer = MoveDirectionPlayer.Up;
        }

        void MoveDown()
        {
            moveDirectionPlayer = MoveDirectionPlayer.Down;
        }

        void MoveLeft()
        {
            moveDirectionPlayer = MoveDirectionPlayer.Left;
        }

        internal void MoveRight()
        {
            moveDirectionPlayer = MoveDirectionPlayer.Right;
        }

        internal void StopMoving()
        {
            moveDirectionPlayer = MoveDirectionPlayer.None;
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
    }
}
