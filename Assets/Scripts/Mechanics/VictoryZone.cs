using UnityEngine;
using UnityEngine.Playables;

namespace Platformer.Mechanics
{
    /// <summary> Marks a trigger as a VictoryZone, usually used to end the current game level. </summary>
    public class VictoryZone : BasePlayerColliderTrigger
    {
        VictoryCondition victoryCondition;

        bool shortcutActivateDeactivated = true;
        
        private PlayableDirector cutscene;
        [SerializeField] private PlayableDirector Victory1;
        [SerializeField] private PlayableDirector Victory2;
      
        
        protected override void DoEnterTriggerAction()
        {
            CheckVictoryCondition();
            Celebrate();
            Play();
        }

        void CheckVictoryCondition()
        {
            // Victory Condition 1: no shortcut, no token, no flower
            if (shortcutActivateDeactivated && !player.token.allTokensCollected)
            {
                victoryCondition = VictoryCondition.Victory1;
                Debug.Log($"Victory Condition 1");
            }

            // Victory Condition 2: no shortcut, yes token, no flower
            if (shortcutActivateDeactivated && player.token.allTokensCollected)
            {
                victoryCondition = VictoryCondition.Victory2;
                Debug.Log($"Victory Condition 2");
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
                case VictoryCondition.Victory1:
                {
                    Debug.Log($"Celebrating Victory 1");
                    cutscene = Victory1;
                    break;
                }
                case VictoryCondition.Victory2:
                {
                    Debug.Log($"Celebrating Victory 2");
                    cutscene = Victory2;
                    break;
                }
                // case VictoryCondition.Shortcut:
                // {
                //     // TODO use playerRigidBody.rotation = 50f; to make seem that is running away
                //     break;
                // }
                // case VictoryCondition.TokenShortcut:
                // {
                //     break;
                // }
            }
        }

        private void Play()
        {
            player.DisableInput();
            Debug.Log($"Entered Victory1 Class");
            cutscene.Play();
        }

        private enum VictoryCondition
        {
            Victory1,
            Victory2,
            // Shortcut,
            // TokenShortcut
        }

        protected override void DoExitTriggerAction()
        {
            // throw new System.NotImplementedException();
        }

    }
}
