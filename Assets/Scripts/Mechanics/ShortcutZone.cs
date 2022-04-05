using Platformer.Core;
using Platformer.Gameplay;

namespace Platformer.Mechanics
{
    /// <summary> When Player enters the Shortcutzone, Big Boss will jump towards the Player, Player will turn to
    /// face Big Boss then Big Boss will become bigger, Player will get scared and start running. </summary>
    public class ShortcutZone : BasePlayerColliderTrigger
    {
        public BigBossController bigBoss;
        private bool playerEnteredZone;

        protected override void DoEnterTriggerAction()
        {
            playerEnteredZone = true;
        }

        private void Update()
        {
            if (!playerEnteredZone) return;

            if (!bigBoss.scareJump)
                MakeBigBossJump();

            if (bigBoss.scareJump && !bigBoss.canBecomeBigger &&
                bigBoss.jumpState == JumpStatePlayer.Landed)
                MakeBigBossBigger();

            if (bigBoss.canBecomeBigger && bigBoss.scareJump && player.playerScared == false &&
                player.playerJumpScaredRan == false)
                MakePlayerScared();
        }

        private void MakeBigBossJump()
        {
            // TODO should this line be here, since it is the only place that will use it? Or is there a better place to put it?
            bigBoss.scareJump = true;
            var ej = Simulation.Schedule<BigBossJump>();
            ej.shortcutZone = this;
        }

        private void MakeBigBossBigger()
        {
            player.FlipPlayerToFaceEast();
            var bbb = Simulation.Schedule<BigBossBigger>(2f);
            bbb.shortcutZone = this;
        }

        private void MakePlayerScared()
        {
            var ps = Simulation.Schedule<PlayerScared>();
            ps.shortcutZone = this;
        }

        protected override void DoExitTriggerAction()
        {
            // throw new System.NotImplementedException();
        }
    }
}
