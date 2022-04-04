using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Base class for Collider triggers with all the basic logic necessary for PlayerController.
    /// </summary>
    public abstract class BasePlayerColliderTrigger : MonoBehaviour
    {
        protected abstract void DoEnterTriggerAction();
        protected abstract void DoExitTriggerAction();
        internal PlayerController player;

        void OnTriggerEnter2D(Collider2D other)
        {
            var rb = other.attachedRigidbody;
            if (rb == null) return;

            if (other == null) return;
            if (!CheckIfItIsPlayer(other)) return;

            player = GetPlayerController(other);
            DoEnterTriggerAction();
        }

        void OnTriggerExit2D(Collider2D other)
        {
            var rb = other.attachedRigidbody;
            if (rb == null) return;

            if (other == null) return;
            if (!CheckIfItIsPlayer(other)) return;

            player = GetPlayerController(other);
            DoExitTriggerAction();
        }

        static PlayerController GetPlayerController(Collider2D other)
        {
            return other.attachedRigidbody.GetComponent<PlayerController>();
        }

        static bool CheckIfItIsPlayer(Collider2D other)
        {
            return other.attachedRigidbody.CompareTag("Player");
        }
    }
}
