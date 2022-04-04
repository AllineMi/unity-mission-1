using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Fired when a player enters a trigger with a ShortcutZone component.
    /// </summary>
    public class EnablePlayerCollider : Event<EnablePlayerCollider>
    {
        internal PlayerController player;

        public override void Execute()
        {
            player.EnableCollider();
        }
    }
}
