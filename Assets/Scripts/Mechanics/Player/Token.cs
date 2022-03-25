using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// When player collects more than 10 tokens player sprite should get rounder
    /// </summary>
    public class Token : MonoBehaviour
    {
        /// <summary>
        /// The maximum token points for the entity.
        /// </summary>
        public int maxToken = 20;
        public int currentToken;
        protected internal bool allTokensCollected = false;

        /// <summary>
        /// Increment the Token of the entity.
        /// </summary>
        public void Increment()
        {
            currentToken = Mathf.Clamp(currentToken + 1, 0, maxToken);
            if (currentToken == 20)
            {
                BecomeRounder();
            }
        }

        public void BecomeRounder()
        {
            PlatformerModel model = Simulation.GetModel<PlatformerModel>();
            PlayerController player = model.player;
            if (currentToken != 20) return;

            allTokensCollected = true;
            player.animator.runtimeAnimatorController = player.playerControllerTokens;
        }
    }
}
