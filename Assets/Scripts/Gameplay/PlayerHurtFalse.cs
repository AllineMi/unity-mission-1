using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;

namespace Gameplay
{
    public class PlayerHurtFalse : Simulation.Event<PlayerHurtFalse>
    {
        internal PlayerController player;

        public override void Execute()
        {
            player.playerHurt = false;
        }
    }
}

