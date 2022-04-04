using UnityEngine;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;

[RequireComponent(typeof(BoxCollider2D))]
public class TeleportPad : BasePlayerColliderTrigger
{
    [Tooltip("Check it if you want this pad to be used only as a destination and never .the only one enabled.")]
    public bool disableDepartures;
    public GameObject destinationPad;
    internal bool isDestination;

    protected override void DoEnterTriggerAction()
    {
        if (isDestination || disableDepartures) return; // Will leave if Pad is the destination
        ScheduleTeletransportation();
    }

    protected override void DoExitTriggerAction()
    {
        if (!isDestination) return; // Will leave if Pad is not the destination
        UnlockDestination();
    }

    void ScheduleTeletransportation()
    {
        var ev = Schedule<Teletransportation>();
        ev.destinationPad = destinationPad;
        ev.playerController = player;
    }

    /// <summary> Unlocks the Teleport Pad so it can be used again. </summary>
    void UnlockDestination()
    {
        isDestination = false;
    }
}
