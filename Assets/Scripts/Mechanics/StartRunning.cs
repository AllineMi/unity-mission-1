﻿using UnityEngine;
using Platformer.Core;

namespace Platformer.Mechanics
{
    public class StartRunning : Simulation.Event<StartRunning>
    {
        public ShortcutZone shortcutZone;
        private PlayerController player;
        private BigBossController bigBoss;

        public override void Execute()
        {
            player = shortcutZone.player;
            bigBoss = shortcutZone.bigBoss;
            player.FlipPlayerToFaceWest();

            // TODO use playerRigidBody.rotation = 50f; to make seem that is running away
            //player.GetComponent<Rigidbody2D>().rotation = 50f;
            Rigidbody2D playerRigidBody = player.animator.GetComponent<Rigidbody2D>();
            playerRigidBody.velocity = new Vector2(2f, 0f);

            //bigBoss.velocity = new Vector2(2f, 0f);
        }
    }
}
