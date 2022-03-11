using System;
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
        public PlatformerJumpPad jumpPad;
        
        void OnTriggerEnter2D(Collider2D collider)
        {
            var player = collider.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                if (player.token.allTokensCollected)
                {
                    Debug.Log("All tokens collected!");
                    jumpPad.collider2d.enabled = true;
                }
                
                Debug.Log("NOT all tokens collected!");
                var ev = Schedule<PlayerEnteredVictoryZone>();
                ev.victoryZone = this;
            }
        }
    }
}