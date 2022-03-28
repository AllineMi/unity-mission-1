using UnityEngine;
using Platformer.Mechanics;

namespace Mechanics
{
    public class ShortcutElevatorZone : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            var rb = other.attachedRigidbody;
            if (rb == null) return;

            var player = rb.GetComponent<PlayerController>();
            if (player == null) return;

            Debug.Log($"Elevator Zone");
            player.DisableInput();
            player.MoveRight();
        }
    }
}
