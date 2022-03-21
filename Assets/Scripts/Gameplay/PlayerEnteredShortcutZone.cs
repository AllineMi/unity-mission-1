using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a player enters a trigger with a ShortcutZone component.
    /// </summary>
    /// <typeparam name="PlayerEnteredShortcutZone"></typeparam>
    public class PlayerEnteredShortcutZone : Simulation.Event<PlayerEnteredShortcutZone>
    {
        public ShortcutZone shortcutZone;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            Simulation.Schedule<PlayerDeath>(0);
        }
    }
}