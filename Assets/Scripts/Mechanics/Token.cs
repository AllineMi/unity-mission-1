using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary> When player collects more than 10 tokens player sprite should get rounder </summary>
    public class Token : MonoBehaviour
    {
        public bool allTokensCollected => currentToken == maxToken;

        /// <summary> The maximum token points </summary>
        private const int maxToken = 20;

        /// <summary> The current token points </summary>
        public int currentToken;

        /// <summary> Increment the Token of the entity </summary>
        public void Increment()
        {
            currentToken = Mathf.Clamp(currentToken + 1, 0, maxToken);
        }
    }
}
