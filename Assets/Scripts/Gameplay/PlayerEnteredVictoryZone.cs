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
        private FriendController friend;

        private bool colletedAllTokens;
        private VictoryCondition victoryCondition;
        private bool shortcutActivateDeactivated = true;

        public override void Execute()
        {
            player = victoryZone.player;
            friend = victoryZone.friend;
            if (player.token.currentToken >= 20) colletedAllTokens = true;
            player.controlEnabled = false;
            model.virtualCamera.enabled = false;

            if (friend == null) Debug.Log($"PlayerEnteredVictoryZone: Friend is NULL");
            if (player == null) Debug.Log($"PlayerEnteredVictoryZone: Player is NULL");
            CheckVictoryCondition();
            Celebrate();
        }

        void CheckVictoryCondition()
        {
            // no shortcut, no token, no flower
            if (shortcutActivateDeactivated && !colletedAllTokens)
            {
                victoryCondition = VictoryCondition.Default;
            }

            // no shortcut, yes token, no flower
            if (shortcutActivateDeactivated && colletedAllTokens)
            {
                victoryCondition = VictoryCondition.Token;
            }

            //victoryCondition = shortcutActivate == false ? VictoryCondition.Default : VictoryCondition.Shortcut;
            //
            // // "yes shortcut, no token"
            // if (shortcutActivate && player.token.currentToken < 20) victoryCondition = VictoryCondition.Shortcut;
            // // "yes shortcut, yes token"
            // if (!shortcutActivate || player.token.currentToken == 20) victoryCondition = VictoryCondition.TokenShortcut;
            //
            // if (player.token.currentToken < 20) victoryCondition = shortcutActivate == false ? VictoryCondition.Default : VictoryCondition.Shortcut;

            // if (player.token.currentToken == 20) victoryCondition = shortcutActivate == false ? VictoryCondition.Token : VictoryCondition.TokenShortcut;
        }

        private void Celebrate()
        {
            Debug.Log($"PlayerEnteredVictoryZone: victoryCondition {victoryCondition}");
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
                case VictoryCondition.Token:
                {
                    var pvr = Schedule<PlayerVictoryTokenRun>();
                    pvr.victoryZone = victoryZone;
                    // var pv = Schedule<PlayerVictory>();
                    // pv.victoryZone = victoryZone;
                    // var pvr = Schedule<PlayerVictoryTokenRun>(3f);
                    // pvr.victoryZone = victoryZone;
                    break;
                }
                case VictoryCondition.Shortcut:
                {
                    // TODO use playerRigidBody.rotation = 50f; to make seem that is running away
                    break;
                }
                case VictoryCondition.TokenShortcut:
                {
                    break;
                }
            }
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
