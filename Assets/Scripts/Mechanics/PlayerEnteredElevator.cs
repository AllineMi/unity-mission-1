using Mechanics;
using Platformer.Core;
using UnityEngine;
using UnityEngine.Animations;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerEnteredElevator : MonoBehaviour
    {
        public PlayerController player;
        public Elevator elevator;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log($"PlayerEnteredElevator");
                player.StopMoving();
                player.stayOnElevator = true;
                player.transform.parent = elevator.transform;
                var meu = Simulation.Schedule<MoveElevatorUp>(1f);
                meu.elevator = elevator;
            }
        }
    }
}
