﻿using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// DeathZone components mark a collider which will schedule a
    /// PlayerEnteredDeathZone event when the player enters the trigger.
    /// </summary>
    public class DeathZone : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collider)
        {
            var player = collider.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.health.Die();
                var ev = Schedule<PlayerEnteredDeathZone>();
                ev.deathzone = this;
            }
        }
    }
}