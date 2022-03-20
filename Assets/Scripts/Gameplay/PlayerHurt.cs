using System;
using System.Numerics;
using System.Runtime.InteropServices;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player gets hurt.
    /// </summary>
    /// <typeparam name="PlayerHurt"></typeparam>
    public class PlayerHurt : Simulation.Event<PlayerHurt>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;
            
            if (player.health.IsAlive)
            {
                player.health.Hurt();
                model.virtualCamera.m_Follow = null;
                model.virtualCamera.m_LookAt = null;
                //player.collider2d.enabled = false;
                player.controlEnabled = false;

                if (player.audioSource && player.ouchAudio)
                    player.audioSource.PlayOneShot(player.ouchAudio);
                player.animator.SetTrigger("hurt");
                
                TeleportPlayerHurtPosition(player);
                
                Simulation.Schedule<EnablePlayerInput>(1f);
            }
        }

        /// <summary>
        /// When the player gets hurt, it will move away from the enemy.
        /// </summary>
        /// <param name="playerController"></param>
        private static void TeleportPlayerHurtPosition(PlayerController player)
        {
            // Player current position
            var playerPosition = player.transform.position;
            var playerXPosition = playerPosition.x;
            var playerYPosition = playerPosition.y;
            float playerNewXPosition;
            const float distanceToBeTeleported = 1.0f;
            // TODO also check where enemy collides (player's front or back) and move away from it)
            if (player.spriteRenderer.flipX) // If Player is facing East
            {
                playerNewXPosition = playerXPosition + distanceToBeTeleported;
            }
            else // If Player is facing West
            {
                playerNewXPosition = playerXPosition - distanceToBeTeleported;
            }

            Vector3 playerNewPosition = new Vector3(playerNewXPosition, playerYPosition);
            player.TeleportHurt(playerNewPosition);
        }
    }
}