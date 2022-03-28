using UnityEngine;
using UnityEngine.EventSystems;

namespace Mechanics
{
    public class MyMoveDirectionsLeft : MonoBehaviour
    {
        /// <summary>
        /// This is the Left state of MoveDirection. Assign functionality for moving to the left.
        /// </summary>
        /// <remarks>
        /// Use the Left state for an easily identifiable way of moving a GameObject to the left (-1 , 0 , 0).
        /// This is a state without any predefined functionality. Before using this state, you should define what your
        /// GameObject will do in code.
        /// </remarks>
        /// <example>
        /// <code>
        //Assign this script to a visible GameObject (with a Rigidbody attached) to see this in action
        Vector3 m_StartPosition, m_StartForce;

        Rigidbody2D m_Rigidbody;

        //Use Enum for easy switching between direction states
        MoveDirection m_MoveDirection;

        //Use these Vectors for moving Rigidbody components
        Vector3 m_ResetVector;
        Vector3 m_RightVector;
        const float speed = 5.0f;

        void Start()
        {
            //You get the Rigidbody component attached to the GameObject
            m_Rigidbody = GetComponent<Rigidbody2D>();
            //This starts with the Rigidbody not moving in any direction at all
            m_MoveDirection = MoveDirection.None;

            //These are the GameObject’s starting position and Rigidbody position
            m_StartPosition = transform.position;
            m_StartForce = m_Rigidbody.transform.position;

            //This Vector is set to 1 in the x axis (for moving in the right direction)
            m_RightVector = Vector3.right;
            //This Vector is zeroed out for when the Rigidbody should not move
            m_ResetVector = Vector3.zero;
        }

        void Update()
        {
            //This switches the direction depending on button presses
            switch (m_MoveDirection)
            {
                //The starting state which resets the object
                case MoveDirection.None:
                    //Reset to the starting position of the GameObject and Rigidbody
                    transform.position = m_StartPosition;
                    m_Rigidbody.transform.position = m_StartForce;
                    //This resets the velocity of the Rigidbody
                    m_Rigidbody.velocity = m_ResetVector;
                    break;

                //This is for moving left
                case MoveDirection.Left:
                    //This moves the Rigidbody to the left (minus right Vector)
                    m_Rigidbody.velocity = -m_RightVector * speed;
                    break;
            }
        }

        void OnGUI()
        {
            //Press the reset Button to switch to no mode
            if (GUI.Button(new Rect(100, 0, 150, 30), "Reset"))
            {
                //Switch to start/reset case
                m_MoveDirection = MoveDirection.None;
            }

            //Press the Left button to switch the Rigidbody direction to the left
            if (GUI.Button(new Rect(100, 30, 150, 30), "Move Left"))
            {
                //Switch to the left direction
                m_MoveDirection = MoveDirection.Left;
            }
        }
    }
}
