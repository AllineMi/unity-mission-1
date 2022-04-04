using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary> Fired when an enemy gets hurt. </summary>
    public class HurtBigBoss : Simulation.Event<HurtBigBoss>
    {
        internal BigBossController bigBoss;

        public override void Execute()
        {
            // bigBoss.TriggerHurt();
            //
            // if (bigBoss.audioSourceBigBoss && bigBoss.ouchAudioBigBoss)
            // {
            //     bigBoss.BigBossHurtAudio();
            // }
        }
    }
}
