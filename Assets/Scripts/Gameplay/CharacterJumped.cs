using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player performs a Jump.
    /// </summary>
    public class CharacterJumped : Simulation.Event<CharacterJumped>
    {
        public PlayerController player;
        public FriendController friend;
        private MyCharacterController characterController;

        public override void Execute()
        {
            if (characterController != null)
            {
                if (characterController.audioSource && characterController.jumpAudio)
                    characterController.audioSource.PlayOneShot(characterController.jumpAudio);
            }

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
