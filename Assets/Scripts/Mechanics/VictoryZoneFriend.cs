using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
namespace Platformer.Mechanics
{
    public class VictoryZoneFriend : MonoBehaviour
    {
        public PlayerController friend;
        void OnTriggerEnter2D(Collider2D other)
        {
            var rb = other.attachedRigidbody;
            if (rb != null)
            {
                friend.animator.SetTrigger("victory");
                friend.controlEnabled = false;
            }
        }
    }
}