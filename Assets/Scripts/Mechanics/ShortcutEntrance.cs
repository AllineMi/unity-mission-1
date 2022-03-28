using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    public class ShortcutEntrance : MonoBehaviour
    {
        internal PlayerController player;
        public bool shortcutActivated = false;

        void OnTriggerEnter2D(Collider2D collider)
        {
            player = collider.gameObject.GetComponent<PlayerController>();
            if (player == null) return;

            if (shortcutActivated == false)
            {
                player.DisableInput();
                player.collider2d.enabled = false;
                player.spriteRenderer.flipX = false;

                shortcutActivated = true;

                var a = Schedule<EnablePlayerComponents>(1f);
                a.player = player;
            }
        }

    }
}
