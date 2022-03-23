using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {
        public PlayerController player;
        public FriendController friend;

        void OnTriggerEnter2D(Collider2D other)
        {
            var rb = other.attachedRigidbody;
            if (rb == null) return;

            player = rb.GetComponent<PlayerController>();

            if (player == null) return;
            var ev = Schedule<PlayerEnteredVictoryZone>();
            ev.victoryZone = this;
        }
    }
}
