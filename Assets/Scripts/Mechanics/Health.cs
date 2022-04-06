using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary> Represents the current vital statistics of some game entity. </summary>
    public class Health : MonoBehaviour
    {
        /// <summary> The maximum hit points for the entity. </summary>
        int maxHP = 10;

        public int currentHP;

        /// <summary> Indicates if the entity should be considered 'alive'. </summary>
        internal bool IsAlive => currentHP > 0;

        /// <summary> Decrement the HP of the entitiy. </summary>
        internal void Hurt()
        {
            Decrement();
        }

        /// <summary> Decrement the HP of the entitiy until HP reaches 0. </summary>
        internal void Die()
        {
            while (currentHP > 0) Decrement();
        }

        /// <summary> Increment the HP of the entity. </summary>
        internal void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        /// <summary> Decrement the HP of the entity. Will trigger a HealthIsZero event when current HP reaches 0. </summary>
        internal void Decrement()
        {
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
        }
    }
}
