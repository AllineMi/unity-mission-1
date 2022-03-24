using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;

namespace Gameplay
{
    public class BigBossBigger : Simulation.Event<BigBossBigger>
    {
        public ShortcutZone shorcutZone;
        public PlayerController player;
        public BigBossController bigBoss;

        public override void Execute()
        {
            bigBoss = shorcutZone.bigBoss;
            Rigidbody2D bigBossRigidBody = bigBoss.animator.GetComponent<Rigidbody2D>();
            bigBoss.animator.runtimeAnimatorController = bigBoss.BigBossBigger;
            //bigBoss.transform.Translate(3.5f, 3.5f, 1f, Space.Self);
            bigBossRigidBody.transform.localScale = new Vector3(3.5f, 3.5f, 1f);
            ;
            //bigBoss.transform.localScale = new Vector3(3.5f, 3.5f, 1f);
            bigBoss.getBigger = true;
        }
    }
}
