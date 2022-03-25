using Gameplay;
using static Platformer.Core.Simulation;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// ShortcutZone components mark a collider which will schedule a
    /// EnablePlayerComponents event when the player enters the trigger.
    /// </summary>
    public class ShortcutZone : MonoBehaviour
    {
        public PlayerController player;
        public BigBossController bigBoss;
        private bool jumpScaredRan;

        void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"ShortcutZone, Collider: {other.gameObject.name}");

            var rb = other.attachedRigidbody;
            if (rb == null) return;
            if (player == null) return;

            if (other.gameObject.CompareTag("Player") && bigBoss.canBecomeBigger == false && player.scared == false)
            {
                var ej = Schedule<BigBossJump>();
                ej.shortcutZone = this;
            }
            // var ev = Schedule<PlayerEnteredShortcutZone>();
            // ev.shortcutZone = this;
        }

        private void Update()
        {
            if (bigBoss.jumpState == BigBossController.JumpState.Landed)
            {
                player.FlipPlayerX();
                // TODO add dialogue saying that the monster is not scary. after that, monster gets bigger
                var bbb = Schedule<BigBossBigger>(2f);
                bbb.shortcutZone = this;
            }

            if (bigBoss.canBecomeBigger && player.scared == false && jumpScaredRan == false)
            {
                jumpScaredRan = true;
                var ps = Schedule<PlayerScared>();
                ps.shortcutZone = this;
            }
        }
    }
}
