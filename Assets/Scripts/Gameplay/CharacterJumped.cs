using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using Object = System.Object;

namespace Platformer.Gameplay
{
    public class CharacterJumped : Simulation.Event<CharacterJumped>
    {
        PlayerController player;
        FriendController friend;
        public BaseController baseController;

        public override void Execute()
        {
            player = baseController.platformerModel.player;
            friend = baseController.platformerModel.friend;

            if (player != null && player.jumpState == JumpState.InFlight)
            {
                if (player.audioSourcePlayer && player.jumpAudioPlayer)
                    player.PlayJumpAudio();
            }

            if (friend != null && friend.jumpState == JumpState.InFlight)
            {
                if (friend.audioSource && friend.jumpAudio)
                    friend.PlayJumpAudio();
            }
        }
    }
}
