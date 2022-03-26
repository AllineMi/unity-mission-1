using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

abstract public class MyCharacterController : KinematicObject
{
    public float jumpTakeOffSpeed = 10;
    public AudioClip jumpAudio;
    public AudioSource audioSource;
    public MyJumpState myJumpState = MyJumpState.Grounded;
    private bool stopJump;
    public bool myJump;
    private Animator animator;
    public float maxSpeed = 5;
    private Vector2 move;
    readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();
    public SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        UpdateMyJumpState();
        base.Update();
    }

    public void Jump(float jumpSpeed)
    {
        myJumpState = MyJumpState.PrepareToJump;
        move.x = jumpSpeed;
    }

    public void Jump()
    {
        Jump(jumpTakeOffSpeed);
    }

    void UpdateMyJumpState()
    {
        myJump = false;
        switch (myJumpState)
        {
            case MyJumpState.PrepareToJump:
                myJump = true;
                myJumpState = MyJumpState.Jumping;
                stopJump = false;
                break;
            case MyJumpState.Jumping:
                if (!IsGrounded)
                {
                    Simulation.Schedule<CharacterJumped>();
                    myJumpState = MyJumpState.InFlight;
                }

                break;
            case MyJumpState.InFlight:
                if (IsGrounded)
                {
                    Simulation.Schedule<PlayerLanded>();
                    myJumpState = MyJumpState.Landed;
                }

                break;
            case MyJumpState.Landed:
                // animator.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                StopMoving();
                myJumpState = MyJumpState.Grounded;
                break;
        }
    }

    public void StopMoving()
    {
        move.x = 0f;
    }

    public enum MyJumpState
    {
        Grounded,
        PrepareToJump,
        Jumping,
        InFlight,
        Landed
    }
    private int frames = 0;
    protected override void ComputeVelocity()
    {
        if (myJump && IsGrounded)
        {
            velocity.y = jumpTakeOffSpeed * model.jumpModifier;
            myJump = false;
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

        if (frames % 100 == 0)
        {
            Debug.Log($"MyCharacterController move.x: {move.x} #{gameObject.name}#");
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
}
