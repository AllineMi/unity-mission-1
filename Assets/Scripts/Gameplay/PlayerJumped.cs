using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player performs a Jump.
    /// </summary>
    /// <typeparam name="PlayerJumped"></typeparam>
    public class PlayerJumped : Simulation.Event<PlayerJumped>
    {
        public PlayerController player;
        public FriendController friend;

        public override void Execute()
        {
            if (player != null)
            {
                if (player.audioSource && player.jumpAudio)
                    player.audioSource.PlayOneShot(player.jumpAudio);
            }

            if (friend != null)
            {
                if (friend.audioSource && friend.jumpAudio)
                    friend.audioSource.PlayOneShot(friend.jumpAudio);
            }
        }
    }
}
