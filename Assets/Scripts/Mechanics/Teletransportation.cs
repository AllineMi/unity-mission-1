using Platformer.Core;
using Platformer.Gameplay;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This event is triggered when the player character enters a trigger with a TeleportPad component.
    /// </summary>
    /// <typeparam name="Teletransportation"></typeparam>
    public class Teletransportation : Simulation.Event<Teletransportation>
    {
        internal GameObject destinationPad;
        internal PlayerController playerController;

        public override void Execute()
        {
            LockDestination();

            DisablePlayerControl();

            TurnPlayerEast();

            var playerDestination = GetPlayerDestination();
            TeleportPlayer(playerDestination);

            EnablePlayerControl();
        }

        private void LockDestination()
        {
            destinationPad.GetComponent<TeleportPad>().isDestination = true;
        }

        private void TurnPlayerEast()
        {
            playerController.FlipPlayerToFaceWest();
        }

        private void DisablePlayerControl()
        {
            playerController.DisableInput();
        }

        private Vector3 GetPlayerDestination()
        {
            return destinationPad.transform.position;
        }

        private void TeleportPlayer(Vector3 playerDestination)
        {
            playerController.Teleport(playerDestination);
        }

        private static void EnablePlayerControl()
        {
            Simulation.Schedule<EnablePlayerInput>(1f); // Enables player control
        }
    }
}
