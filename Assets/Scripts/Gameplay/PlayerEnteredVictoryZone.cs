using Platformer.Core;
using Platformer.Model;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    /// <summary> This event is triggered Player enters the VictoryZone. </summary>
    public class PlayerEnteredVictoryZone : Event<PlayerEnteredVictoryZone>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        VictoryCondition victoryCondition;

        public VictoryZone victoryZone;
        PlayerController player;

        bool shortcutActivateDeactivated = true;

        public override void Execute()
        {
            player = victoryZone.player;

            player.DisableInput();

            model.virtualCamera.enabled = false;

            CheckVictoryCondition();
            Celebrate();
        }

        void CheckVictoryCondition()
        {
            // no shortcut, no token, no flower
            if (shortcutActivateDeactivated && !player.token.allTokensCollected)
            {
                victoryCondition = VictoryCondition.Default;
            }

            // no shortcut, yes token, no flower
            if (shortcutActivateDeactivated && player.token.allTokensCollected)
            {
                victoryCondition = VictoryCondition.Token;
            }

            //victoryCondition = shortcutActivate == false ? VictoryCondition.Default : VictoryCondition.Shortcut;
            // // "yes shortcut, no token"
            // if (shortcutActivate && player.token.currentToken < 20) victoryCondition = VictoryCondition.Shortcut;
            // // "yes shortcut, yes token"
            // if (!shortcutActivate || player.token.currentToken == 20) victoryCondition = VictoryCondition.TokenShortcut;
            // if (player.token.currentToken < 20) victoryCondition = shortcutActivate == false ? VictoryCondition.Default : VictoryCondition.Shortcut;
            // if (player.token.currentToken == 20) victoryCondition = shortcutActivate == false ? VictoryCondition.Token : VictoryCondition.TokenShortcut;
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
                case VictoryCondition.Token:
                {
                    var pv = Schedule<PlayerVictory>();
                    pv.victoryZone = victoryZone;

                    var pvt = Schedule<FriendVictoryJump>(2f);
                    pvt.victoryZone = victoryZone;

                    var pvr = Schedule<PlayerVictoryRun>(3f);
                    pvr.victoryZone = victoryZone;
                    pvr.player = player;
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
