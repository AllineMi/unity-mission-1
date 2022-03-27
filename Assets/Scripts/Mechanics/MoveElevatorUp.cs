using Platformer.Core;
using UnityEngine;

namespace Mechanics
{
    public class MoveElevatorUp : Simulation.Event<MoveElevatorUp>
    {
        public Elevator elevator;

        public override void Execute()
        {
            Debug.Log($"Move elevator up");
            elevator.MoveElevatorUp();
            elevator.activateElevator = true;
        }
    }
}
