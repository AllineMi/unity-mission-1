﻿using static Platformer.Core.Simulation;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class BigBossJump : Event<BigBossJump>
    {
        private float bigBossJumpVelocity = 5f;
        public ShortcutZone shortcutZone;
        private BigBossController bigBoss;
        private Rigidbody2D bigBossRigidBody;

        public override void Execute()
        {
            bigBoss = shortcutZone.bigBoss;
            bigBossRigidBody = bigBoss.GetComponent<Rigidbody2D>();
            bigBoss.jumpState = BigBossController.JumpState.PrepareToJump;
            bigBossRigidBody.velocity = new Vector2(bigBossJumpVelocity, 0f);
        }
    }
}
