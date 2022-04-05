using UnityEngine;

namespace Platformer.Mechanics
{
    public class MyMoveDirectionsNone : MonoBehaviour
    {
        /// <summary>
        /// This is the None state. Assign functionality that stops movement.
        /// </summary>
        /// <remarks>
        /// Use the None state for an easily identifiable way of stopping, resetting or initialising a GameObject's movement. This is a state without any predefined functionality. Before using this state, you should define what your GameObject will do in code.
        /// </remarks>

        //Attach this script to a GameObject with a Rigidbody attached. This script starts off on the ModeDirection.None state but changes depending on buttons you press.
        Vector3 m_StartPosition, m_StartForce;

        Rigidbody m_Rigidbody;

        //Use Enum for easy switching between direction states
        MoveDirection m_MoveDirection;

        //Use these Vectors for moving Rigidbody components
        Vector3 m_ResetVector;
        Vector3 m_UpVector;
        const float speed = 10.0f;

        void Start()
        {
            //You get the Rigidbody component attached to the GameObject
            m_Rigidbody = GetComponent<Rigidbody>();
            //This starts with the Rigidbody not moving in any direction at all
            m_MoveDirection = MoveDirection.None;

            //These are the GameObject’s starting position and Rigidbody position
            m_StartPosition = transform.position;
            m_StartForce = m_Rigidbody.transform.position;

            //This Vector is set to 1 in the y axis (for moving upwards)
            m_UpVector = Vector3.up;
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

                //This is for moving down
                case MoveDirection.Down:
                    //This moves the Rigidbody down
                    m_Rigidbody.velocity = -m_UpVector * speed;
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

            //Press the Down button to switch the direction to down
            if (GUI.Button(new Rect(100, 90, 150, 30), "Move Down"))
            {
                //Switch to Down Direction
                m_MoveDirection = MoveDirection.Down;
            }
        }
    }
}
