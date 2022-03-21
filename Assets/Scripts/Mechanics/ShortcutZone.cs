using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// ShortcutZone components mark a collider which will schedule a
    /// PlayerEnteredShortcutZone event when the player enters the trigger.
    /// </summary>
    public class ShortcutZone : MonoBehaviour
    {
        public bool shortcutActivate = false;
        void OnTriggerEnter2D(Collider2D collider)
        {
            var player = collider.gameObject.GetComponent<PlayerController>();
            if (player == null) return;

            shortcutActivate = true;
            var ev = Schedule<PlayerEnteredShortcutZone>();
            //ev.shortcutActivate = this;
        }
    }
}