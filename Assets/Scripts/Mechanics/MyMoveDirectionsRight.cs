using UnityEngine;

namespace Platformer.Mechanics
{
    public class MyMoveDirectionsRight : MonoBehaviour
    {
        /// <summary>
        /// This is the Right state of MoveDirection. Assign functionality for moving to the right.
        /// </summary>
        /// <remarks>
        /// Use the Right state for an easily identifiable way of moving a GameObject to the right (1 , 0 , 0). This is a state without any predefined functionality. Before using this state, you should define what your GameObject will do in code.
        /// </remarks>
        //Attach this script to a GameObject with a Rigidbody component. Press the "Move Right" button in Game view to see it in action.
        Vector3 m_StartPosition, m_StartForce;

        Rigidbody2D m_Rigidbody;

        //Use Enum for easy switching between direction states
        MoveDirection m_MoveDirection;

        //Use these Vectors for moving Rigidbody components
        Vector3 m_ResetVector;
        Vector3 m_RightVector;
        public float speed = 5.0f;

        void Start()
        {
            //You get the Rigidbody component attached to the GameObject
            m_Rigidbody = GetComponent<Rigidbody2D>();
            //These are the GameObject’s starting position and Rigidbody position
            m_StartPosition = transform.position;
            m_StartForce = m_Rigidbody.transform.position;
            //This starts with the Rigidbody not moving in any direction at all
            m_MoveDirection = MoveDirection.None;
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

                //This is for moving right
                case MoveDirection.Right:
                    //This moves the Rigidbody to the right
                    m_Rigidbody.velocity = m_RightVector * speed;
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

            //Press the Left button to switch the Rigidbody direction to the right
            if (GUI.Button(new Rect(100, 30, 150, 30), "Move Right"))
            {
                //Switch to the left direction
                m_MoveDirection = MoveDirection.Right;
            }
        }
    }
}
