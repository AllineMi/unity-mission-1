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
        public MyCharacterController characterController;
        private bool jumpScaredRan;
        private bool playerEnteredZone;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerEnteredZone = true;
            }

            if (player == null) return;

            if (other.gameObject.CompareTag("Player") && bigBoss.canBecomeBigger == false && player.scared == false)
            {
                var ej = Schedule<BigBossJump>();
                ej.shortcutZone = this;
            }
        }

        private void Update()
        {
            if (!playerEnteredZone) return;

            if (bigBoss.myJumpState == MyCharacterController.MyJumpState.Landed)
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
