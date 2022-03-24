using Platformer.Mechanics;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a player enters a trigger with a ShortcutZone component.
    /// </summary>
    /// <typeparam name="PlayerEnteredShortcutZone"></typeparam>
    public class EnablePlayerComponents : Event<EnablePlayerComponents>
    {
        internal PlayerController player;

        public override void Execute()
        {
            player.spriteRenderer.enabled = true;
            player.collider2d.enabled = true;
        }
    }
}
