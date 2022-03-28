using Platformer.Mechanics;
using UnityEngine;

namespace Mechanics
{
    /// <summary>
    /// This is an 4 direction movement enum.
    /// </summary>
    /// <remarks>
    /// MoveDirection provides a way of switching between moving states. You must assign these states to actions,
    /// such as moving the GameObject by an up vector when in the Up state.
    /// Having states like these are easier to identify than always having to include a large amount of vectors and
    /// calculations.Instead, you define what you want the state to do in only one part, and switch to the appropriate
    /// state when it is needed.
    /// </remarks>
    [RequireComponent(typeof(Rigidbody2D))]
    //This is a full example of how a GameObject changes direction using MoveDirection states
    //Assign this script to a visible GameObject (with a Rigidbody attached) to see it in action
    public class MyObjectController : KinematicObject
    {
        public Vector3 m_MyObjectStartForce = new Vector3(1f, 1f, 1f);

        public Rigidbody2D m_MyObjectRigidbody;

        //Use Enum for easy switching between direction states
        MyObjectMoveDirection m_MyObjectMoveDirection;

        //Use these Vectors for moving Rigidbody components
        Vector3 m_MyObjectResetVector;
        Vector3 m_MyObjectUpVector;
        Vector3 m_MyObjectRightVector;
        public float speed = 5.0f;

        void Awake()
        {
            //You get the Rigidbody component attached to the GameObject
            m_MyObjectRigidbody = GetComponent<Rigidbody2D>();
            //This starts with the Rigidbody not moving in any direction at all
            m_MyObjectMoveDirection = MyObjectMoveDirection.None;

            //These are the GameObject’s starting position and Rigidbody position
            m_MyObjectStartForce = m_MyObjectRigidbody.transform.position;

            //This Vector is set to 1 in the y axis (for moving upwards)
            m_MyObjectUpVector = Vector3.up;
            //This Vector is set to 1 in the x axis (for moving in the right direction)
            m_MyObjectRightVector = Vector3.right;
            //This Vector is zeroed out for when the Rigidbody should not move
            m_MyObjectResetVector = Vector3.zero;
        }

        protected override void Update()
        {
            //This switches the direction depending on button presses
            switch (m_MyObjectMoveDirection)
            {
                //The starting state which resets the object
                case MyObjectMoveDirection.None:
                    //Reset to the starting position of the GameObject and Rigidbody
                    m_MyObjectRigidbody.transform.position = m_MyObjectStartForce;
                    //This resets the velocity of the Rigidbody
                    m_MyObjectRigidbody.velocity = m_MyObjectResetVector;
                    break;

                //This is for moving in an upwards direction
                case MyObjectMoveDirection.Up:
                    Debug.Log($"MyMoveObjectDirection.Up: {MyObjectMoveDirection.Up}");
                    //Change the velocity so that the Rigidbody travels upwards
                    m_MyObjectRigidbody.velocity = m_MyObjectUpVector * speed;
                    break;

                //This is for moving left
                case MyObjectMoveDirection.Left:
                    //This moves the Rigidbody to the left (minus right Vector)
                    m_MyObjectRigidbody.velocity = -m_MyObjectRightVector * speed;
                    break;

                //This is for moving right
                case MyObjectMoveDirection.Right:
                    Debug.Log($"Entered MyObjectMoveDirection.Right");
                    //This moves the Rigidbody to the right
                    m_MyObjectRigidbody.velocity = m_MyObjectRightVector * speed;
                    velocity.x = speed;
                    break;

                //This is for moving down
                case MyObjectMoveDirection.Down:
                    //This moves the Rigidbody down
                    m_MyObjectRigidbody.velocity = -m_MyObjectUpVector * speed;
                    break;
            }
        }

        public void MoveUp()
        {
            Debug.Log($"MoveUp");
            m_MyObjectMoveDirection = MyObjectMoveDirection.Up;
        }

        void MoveDown()
        {
            m_MyObjectMoveDirection = MyObjectMoveDirection.Down;
        }

        void MoveLeft()
        {
            m_MyObjectMoveDirection = MyObjectMoveDirection.Left;
        }

        public void MoveRight()
        {
            Debug.Log($"MoveRight");
            m_MyObjectMoveDirection = MyObjectMoveDirection.Right;
        }

        void StopMoving()
        {
            m_MyObjectMoveDirection = MyObjectMoveDirection.None;
        }
    }

    public enum MyObjectMoveDirection
    {
        Left,
        Up,
        Right,
        Down,
        None
    }
}
