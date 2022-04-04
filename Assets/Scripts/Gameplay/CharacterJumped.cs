using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    public class CharacterJumped : Simulation.Event<CharacterJumped>
    {
        internal PlayerController player;
        internal FriendController friend;
        public override void Execute()
        {
            if (player != null)
            {
                if (player.audioSourcePlayer && player.jumpAudioPlayer)
                    player.audioSourcePlayer.PlayOneShot(player.jumpAudioPlayer);
            }

            if (friend != null)
            {
                if (friend.audioSource && friend.jumpAudio)
                    friend.audioSource.PlayOneShot(friend.jumpAudio);
            }
        }
    }
}
