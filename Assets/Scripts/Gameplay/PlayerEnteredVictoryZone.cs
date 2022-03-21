using Platformer.Core;
using static Platformer.Core.Simulation;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// This event is triggered when the player character enters a trigger with a VictoryZone component.
    /// </summary>
    /// <typeparam name="PlayerEnteredVictoryZone"></typeparam>
    public class PlayerEnteredVictoryZone : Event<PlayerEnteredVictoryZone>
    {
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        
        public VictoryZone victoryZone;
        private PlayerController player;
        private PlayerController friend;

        private VictoryCondition victoryCondition;
        public bool shortcutActivate = false;
        
        public override void Execute()
        {
            player = victoryZone.player;
            friend = victoryZone.friend;
            player.controlEnabled = false;
            model.virtualCamera.enabled = false;
            
            if (friend == null || player == null) Debug.Log($"Friend or Player is NULL");
            CheckVictoryCondition();
            Celebrate();
        }

        private void Celebrate()
        {
            switch (victoryCondition)
            {
                case VictoryCondition.Default:
                {
                    var pv = Schedule<PlayerVictory>();
                    pv.victoryZone = victoryZone;
                    var pvr = Schedule<PlayerVictoryRun>(3f);
                    pvr.victoryZone = victoryZone;
                    break;
                }
                case VictoryCondition.Shortcut:
                {
                    break;
                }
                case VictoryCondition.Token:
                {
                    break;
                }
                case VictoryCondition.TokenShortcut:
                {
                    break;
                }
            }
        }

        void CheckVictoryCondition()
        {
            if (player.token.currentToken < 20) victoryCondition = shortcutActivate == false ? VictoryCondition.Default : VictoryCondition.Shortcut;
            if (player.token.currentToken == 20) victoryCondition = shortcutActivate == false ? VictoryCondition.Token : VictoryCondition.TokenShortcut;
        }

        private enum VictoryCondition
        {
            Default,
            Shortcut,
            Token,
            TokenShortcut
        }
    }
}