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

        /*internal new*/
        public AudioSource audioSource;

        /// <summary> Initial jump velocity at the start of a jump. </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        bool jump;

        /*internal new*/
        public Collider2D collider2d;

        public Health health;

        /// <summary> Max horizontal speed of the player. </summary>
        public float maxSpeed = 7;

        Vector2 move;
        public bool controlEnabled = true;
        public SpriteRenderer spriteRenderer;
        internal Animator animator;

        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        //public int tokensCollected = 0;
        public Token token;
        public RuntimeAnimatorController playerControllerTokens;

        // SCARED
        internal bool scared;

        Rigidbody2D m_Rigidbody;
        //Use Enum for easy switching between direction states
        PlayerMoveDirection m_PlayerMoveDirection;

        //Use these Vectors for moving Rigidbody components
        Vector3 m_PlayerResetVector;
        Vector3 m_PlayerUpVector;
        Vector3 m_PlayerRightVector;
        private float speed = 5.0f;
        public bool stayOnElevator;

        void Awake()
        {
            health = GetComponent<Health>();
            token = GetComponent<Token>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            //You get the Rigidbody component attached to the GameObject
            m_Rigidbody = GetComponent<Rigidbody2D>();
            //This starts with the Rigidbody not moving in any direction at all
            m_PlayerMoveDirection = PlayerMoveDirection.None;

            //This Vector is set to 1 in the y axis (for moving upwards)
            m_PlayerUpVector = Vector3.up;
            //This Vector is set to 1 in the x axis (for moving in the right direction)
            m_PlayerRightVector = Vector3.right;
            //This Vector is zeroed out for when the Rigidbody should not move
            m_PlayerResetVector = Vector3.zero;
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                {
                    jumpState = JumpState.PrepareToJump;
                }
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

            if (!controlEnabled)
            {
                //This switches the direction depending on button presses
                switch (m_PlayerMoveDirection)
                {
                    //The starting state which resets the object
                    case PlayerMoveDirection.None:
                        //This resets the velocity of the Rigidbody
                        m_Rigidbody.velocity = m_PlayerResetVector;
                        //m_Rigidbody.velocity = new Vector2(0f,0f);
                        break;

                    //This is for moving in an upwards direction
                    case PlayerMoveDirection.Up:
                        Debug.Log($"PlayerMoveDirection.Up: {PlayerMoveDirection.Up}");
                        //Change the velocity so that the Rigidbody travels upwards
                        //m_Rigidbody.velocity = m_PlayerUpVector * speed;
                        velocity.y = 1f;
                        break;

                    //This is for moving left
                    case PlayerMoveDirection.Left:
                        //This moves the Rigidbody to the left (minus right Vector)
                        m_Rigidbody.velocity = -m_PlayerRightVector * speed;
                        break;

                    //This is for moving right
                    case PlayerMoveDirection.Right:
                        //This moves the Rigidbody to the right
                        m_Rigidbody.velocity = m_PlayerRightVector * speed;
                        velocity.x = speed;
                        break;

                    //This is for moving down
                    case PlayerMoveDirection.Down:
                        //This moves the Rigidbody down
                        m_Rigidbody.velocity = -m_PlayerUpVector * speed;
                        break;
                }
            }

            UpdateJumpState();
            base.Update();
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

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        internal void MoveUp()
        {
            m_PlayerMoveDirection = PlayerMoveDirection.Up;
        }

        void MoveDown()
        {
            m_PlayerMoveDirection = PlayerMoveDirection.Down;
        }

        void MoveLeft()
        {
            m_PlayerMoveDirection = PlayerMoveDirection.Left;
        }

        internal void MoveRight()
        {
            m_PlayerMoveDirection = PlayerMoveDirection.Right;
        }

        internal void StopMoving()
        {
            m_PlayerMoveDirection = PlayerMoveDirection.None;
        }

        void ResetPlayerTakeOffSpeed()
        {
            jumpTakeOffSpeed = 7f;
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

        // TODO to check
        public enum TokenSpriteType
        {
            SpriteNormal,
            SpriteToken10,
            SpriteToken20
        }

        public void Jump(float characterJumpVelocity)
        {
            jumpState = JumpState.PrepareToJump;
            move.x = characterJumpVelocity;
        }

        public void Jump()
        {
            Jump(jumpTakeOffSpeed);
        }

        public void EnableInput()
        {
            controlEnabled = true;
        }

        public void DisableInput()
        {
            controlEnabled = false;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }

    public enum PlayerMoveDirection
    {
        Left,
        Up,
        Right,
        Down,
        None
    }
}
