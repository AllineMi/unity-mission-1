using UnityEngine;

namespace Platformer.Mechanics
{
    public class Elevator : MonoBehaviour
    {
        internal bool activateElevator;
        public GameObject elevatorEntrance;
        public GameObject elevatorExit;
        private Vector3 startJourney;
        private Vector3 endJourney;
        private float timer;
        private const float duration = 1f;

        protected void Awake()
        {
            startJourney = elevatorEntrance.transform.position;
            endJourney = elevatorExit.transform.position;
        }

        protected void Update()
        {
            if (activateElevator) MoveElevatorUp();
        }

        public void MoveElevatorUp()
        {
           timer += Time.deltaTime;
            float fractionOfJourney = timer / duration;

            transform.position = Vector3.MoveTowards(startJourney, endJourney, fractionOfJourney);
        }
    }
}
