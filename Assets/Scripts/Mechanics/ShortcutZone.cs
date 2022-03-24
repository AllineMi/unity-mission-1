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
        private float bigBossJumpVelocity = 13f;

        void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"ShortcutZone: {other.gameObject.name}");

            var rb = other.attachedRigidbody;
            if (rb == null) return;
            if (player == null) return;

            if (other.gameObject.CompareTag("Player") && bigBoss.getBigger == false)
            {
                var ej = Schedule<BigBossJump>();
                ej.shortcutZone = this;
                ej.bigBossJumpVelocity = bigBossJumpVelocity;
            }

            // var ev = Schedule<PlayerEnteredShortcutZone>();
            // ev.shortcutZone = this;
        }

        private void Update()
        {
            if (bigBoss.jumpState == BigBossController.JumpState.Landed)
            {
                player.spriteRenderer.flipX = true;

                bigBoss.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0f);
                // TODO add dialogue saying that the monster is not scary. after that, monster gets bigger
                var bbb = Schedule<BigBossBigger>(1f);
                bbb.shorcutZone = this;
            }

            if (bigBoss.getBigger)
            {
                var ps = Schedule<PlayerScared>();
                ps.shortcutZone = this;
            }
        }
    }
}
