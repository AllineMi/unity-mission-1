using UnityEngine;
using Platformer.Core;
using Platformer.Model;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    abstract public class MyCharacterController : KinematicObject
    {
        public PlatformerModel platformerModel = Simulation.GetModel<PlatformerModel>();

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

        #region JUMP

        public bool jump;
        public bool stopJump;
        public float jumpTakeOffSpeed = 10;
        internal JumpStatePlayer jumpState = JumpStatePlayer.Grounded;
        Rigidbody2D rigidBody;

        #endregion

        #region MOVEMENT

        public bool stopMoving;
        public float maxSpeed = 5;
        public float speed = 5;
        internal Vector2 move;
        MoveDirectionPlayer moveDirection;
        Vector3 resetVector;
        Vector3 rightVector;
        Vector3 upVector;

        #endregion

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            //You get the Rigidbody component attached to the GameObject
            rigidBody = GetComponent<Rigidbody2D>();
            //This starts with the Rigidbody not moving in any direction at all
            moveDirection = MoveDirectionPlayer.None;
            //This Vector is set to 1 in the y axis (for moving upwards)
            upVector = Vector3.up;
            //This Vector is set to 1 in the x axis (for moving in the right direction)
            rightVector = Vector3.right;
            //This Vector is zeroed out for when the Rigidbody should not move
            resetVector = Vector3.zero;
        }

        protected override void Update()
        {
            UpdateMoveDirectionState();
            UpdateJumpState();
            base.Update();
        }

        #region ANIMATION

        /// <summary> SpriteRenderer flip X will be true; </summary>
        private void FlipCharacterToFaceEast()
        {
            spriteRenderer.flipX = true;
        }

        /// <summary> SpriteRenderer flip X will be false; </summary>
        internal void FlipCharacterToFaceWest()
        {
            spriteRenderer.flipX = false;
        }

        internal void PlayRunAnimation()
        {
            animator.SetTrigger("run");
        }

        internal void PlayJumpAnimation()
        {
            jumpState = JumpStatePlayer.PrepareToJump;
        }

        internal void PlayVictoryAnimation()
        {
            animator.SetTrigger("victory");
        }

        #endregion

        #region JUMP

        public abstract void Jump();

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpStatePlayer.PrepareToJump:
                    jumpState = JumpStatePlayer.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpStatePlayer.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<CharacterJumped>();
                        jumpState = JumpStatePlayer.InFlight;
                    }

                    break;
                case JumpStatePlayer.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().characterController = this;
                        jumpState = JumpStatePlayer.Landed;
                    }

                    break;
                case JumpStatePlayer.Landed:
                    StopMoving();
                    jumpState = JumpStatePlayer.Grounded;
                    break;
            }
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
                FlipCharacterToFaceWest();
            }
            else if (move.x < -0.01f || rigidBody.velocity.x < -0.01f)
            {
                FlipCharacterToFaceEast();
            }

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        internal void MoveRight()
        {
            moveDirection = MoveDirectionPlayer.Right;
        }

        internal void MoveLeft()
        {
            moveDirection = MoveDirectionPlayer.Left;
        }

        void MoveUp()
        {
            moveDirection = MoveDirectionPlayer.Up;
        }

        void MoveDown()
        {
            moveDirection = MoveDirectionPlayer.Down;
        }

        internal void StartMoving()
        {
            stopMoving = false;
        }

        internal virtual void StopMoving()
        {
            moveDirection = MoveDirectionPlayer.None;
        }

        void UpdateMoveDirectionState()
        {
            //This switches the direction depending on button presses
            switch (moveDirection)
            {
                //The starting state which resets the object
                case MoveDirectionPlayer.None:
                    //This resets the velocity of the Rigidbody
                    rigidBody.velocity = resetVector;
                    break;

                //This is for moving in an upwards direction
                case MoveDirectionPlayer.Up:
                    //Change the velocity so that the Rigidbody travels upwards
                    rigidBody.velocity = upVector * speed;
                    break;

                //This is for moving left
                case MoveDirectionPlayer.Left:
                    //This moves the Rigidbody to the left (minus right Vector)
                    rigidBody.velocity = -rightVector * speed;
                    break;

                //This is for moving right
                case MoveDirectionPlayer.Right:
                    //This moves the Rigidbody to the right
                    rigidBody.velocity = rightVector * speed;
                    velocity.x = speed;
                    break;

                //This is for moving down
                case MoveDirectionPlayer.Down:
                    //This moves the Rigidbody down
                    rigidBody.velocity = -upVector * speed;
                    break;
            }
        }

        #endregion
    }
}
