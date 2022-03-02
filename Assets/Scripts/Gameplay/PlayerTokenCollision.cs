using System.Runtime.InteropServices;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a player collides with a token.
    /// </summary>
    /// <typeparam name="PlayerCollision"></typeparam>
    public class PlayerTokenCollision : Simulation.Event<PlayerTokenCollision>
    {
        public PlayerController player;
        public TokenInstance token;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        
        public override void Execute()
        {
            AudioSource.PlayClipAtPoint(token.tokenCollectAudio, token.transform.position);
            player.health.Increment();
            //player.token.Increment();
        }
    }
}

// Original
// using Platformer.Core;
// using Platformer.Mechanics;
// using Platformer.Model;
// using UnityEngine;
//
// namespace Platformer.Gameplay
// {
//     /// <summary>
//     /// Fired when a player collides with a token.
//     /// </summary>
//     /// <typeparam name="PlayerCollision"></typeparam>
//     public class PlayerTokenCollision : Simulation.Event<PlayerTokenCollision>
//     {
//         public PlayerController player;
//         public TokenInstance token;
//
//         PlatformerModel model = Simulation.GetModel<PlatformerModel>();
//
//         public override void Execute()
//         {
//             AudioSource.PlayClipAtPoint(token.tokenCollectAudio, token.transform.position);
//         }
//     }
// }