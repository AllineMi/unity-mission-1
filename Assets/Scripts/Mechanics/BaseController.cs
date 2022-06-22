using UnityEngine;
using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Model;

namespace Platformer.Mechanics
{
    public abstract class BaseController : KinematicObject
    {
        internal PlatformerModel platformerModel = Simulation.GetModel<PlatformerModel>();

        #region ANIMATION

        internal Animator animator;
        internal SpriteRenderer spriteRenderer;

        #endregion

        #region AUDIO SHOULD BE ON SPECIF CHARACTER

        #endregion

        #region BOUNDS SHOULD BE ON SPECIF CHARACTER

        #endregion

        #region COLLIDER SHOULD BE ON SPECIF CHARACTER

        #endregion

        #region JUMP - SPECIFICS ON CHARACTER - BASE HERE

        [Header("JUMP SETTINGS")]
        [SerializeField] internal float jumpTakeOffSpeed = 7;
        internal bool jump;
        internal bool stopJump;
        internal JumpState jumpState = JumpState.Grounded;

        #endregion

        #region MOVEMENT

        internal bool stopMoving;
        [SerializeField] internal float maxSpeed = 5;
        [SerializeField] internal float speed = 5;
        internal Vector2 move;
        internal MoveDirections moveDirection;
        internal Vector3 resetVector;
        internal Vector3 rightVector;
        internal Vector3 upVector;
        internal Rigidbody2D rigidBody;

        #endregion

        protected virtual void Awake()
        {
            // ANIMATOR
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            // MOVEMENT
            //This starts with the Rigidbody not moving in any direction at all
            moveDirection = MoveDirections.None;
            //This Vector is set to 1 in the y axis (for moving upwards)
            upVector = Vector3.up;
            //This Vector is set to 1 in the x axis (for moving in the right direction)
            rightVector = Vector3.right;
            //This Vector is zeroed out for when the Rigidbody should not move
            resetVector = Vector3.zero;
            //You get the Rigidbody component attached to the GameObject
            rigidBody = GetComponent<Rigidbody2D>();
        }

        protected override void Update()
        {
            UpdateJumpState();
            UpdateMoveDirectionState();
            base.Update();
        }

        #region ANIMATION

        /// <summary> Checks SpriteRenderer flip X. Player will face West. </summary>
        public void FlipPlayerToFaceWest()
        {
            spriteRenderer.flipX = true;
        }

        /// <summary> Unchecks SpriteRenderer flip X. Player will face east. </summary>
        internal void FlipPlayerToFaceEast()
        {
            spriteRenderer.flipX = false;
        }

        internal void PlayHurtAnimation()
        {
            animator.SetTrigger("hurt");
        }

        internal void PlayVictoryAnimation()
        {
            animator.SetTrigger("victory");
        }

        internal void PlayRunAnimation()
        {
            animator.SetTrigger("run");
        }

        #endregion

        #region JUMP

        public void Jump()
        {
            jumpState = JumpState.PrepareToJump;
        }

        protected void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    JumpStatePrepareToJump();
                    break;
                case JumpState.Jumping:
                    JumpStateJumping();
                    break;
                case JumpState.InFlight:
                    JumpStateInFlight();

                    break;
                case JumpState.Landed:
                    JumpStateLanded();
                    break;
            }
        }

        private void JumpStateLanded()
        {
            jumpState = JumpState.Grounded;
        }

        private void JumpStateInFlight()
        {
            if (IsGrounded)
            {
                Simulation.Schedule<PlayerLanded>().baseController = this;
                jumpState = JumpState.Landed;
            }
        }

        private void JumpStateJumping()
        {
            if (!IsGrounded)
            {
                Simulation.Schedule<CharacterJumped>().baseController = this;
                jumpState = JumpState.InFlight;
            }
        }

        private void JumpStatePrepareToJump()
        {
            jumpState = JumpState.Jumping;
            jump = true;
            stopJump = false;
        }

        #endregion

        #region MOVEMENT

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * platformerModel.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * platformerModel.jumpDeceleration;
                }
            }

            if (move.x > 0.01f || rigidBody.velocity.x > 0.01f)
            {
                FlipPlayerToFaceEast();
            }
            else if (move.x < -0.01f || rigidBody.velocity.x < -0.01f)
            {
                FlipPlayerToFaceWest();
            }

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        internal void StartMoving()
        {
            stopMoving = false;
        }

        internal virtual void StopMoving()
        {
            moveDirection = MoveDirections.None;
        }

        void UpdateMoveDirectionState()
        {
            //This switches the direction depending on button presses
            switch (moveDirection)
            {
                //The starting state which resets the object
                case MoveDirections.None:
                    //This resets the velocity of the Rigidbody
                    rigidBody.velocity = resetVector;
                    break;

                //This is for moving in an upwards direction
                case MoveDirections.Up:
                    //Change the velocity so that the Rigidbody travels upwards
                    rigidBody.velocity = upVector * speed;
                    break;

                //This is for moving left
                case MoveDirections.Left:
                    //This moves the Rigidbody to the left (minus right Vector)
                    rigidBody.velocity = -rightVector * speed;
                    break;

                //This is for moving right
                case MoveDirections.Right:
                    //This moves the Rigidbody to the right
                    rigidBody.velocity = rightVector * speed;
                    velocity.x = speed;
                    break;

                //This is for moving down
                case MoveDirections.Down:
                    //This moves the Rigidbody down
                    rigidBody.velocity = -upVector * speed;
                    break;
            }
        }

        internal void MoveUp()
        {
            moveDirection = MoveDirections.Up;
        }

        internal void MoveDown()
        {
            moveDirection = MoveDirections.Down;
        }

        internal void MoveLeft()
        {
            moveDirection = MoveDirections.Left;
        }

        internal void MoveRight()
        {
            moveDirection = MoveDirections.Right;
        }

        #endregion
    }
}
