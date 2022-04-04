using Platformer.Core;
using UnityEngine;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerEnteredElevator : BasePlayerColliderTrigger
    {
        public Elevator elevator;

        protected override void DoEnterTriggerAction()
        {
            Debug.Log($"PlayerEnteredElevator");
            player.StopMoving();
            player.playerStayOnElevator = true;
            player.transform.parent = elevator.transform;
            var meu = Simulation.Schedule<MoveElevatorUp>(1f);
            meu.elevator = elevator;
        }

        protected override void DoExitTriggerAction()
        {
            throw new System.NotImplementedException();
        }
    }
}
