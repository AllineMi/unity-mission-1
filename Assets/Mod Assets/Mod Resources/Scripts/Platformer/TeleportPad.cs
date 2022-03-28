using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

[RequireComponent(typeof(BoxCollider2D))]
public class TeleportPad : MonoBehaviour
{
    public Rigidbody2D destinationPad;

    [Tooltip("Check it if you want this pad to be used only as a destination and never .the only one enabled.")]
    public bool disableDepartures;

    [HideInInspector] public PlayerController player;
    [HideInInspector] public bool isDestination;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GetPlayerController(other);

        if (isDestination || player == null || disableDepartures) return; // Will leave if Pad is the destination

        //Only triggers if the triggerBody is Player
        var ev = Schedule<Teletransportation>();
        ev.destinationPad = destinationPad;
        ev.playerController = player;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GetPlayerController(other);
        if (!isDestination) return; // Will leave if Pad is not the destination
        if (player == null) return;

        //Only trigger if the triggerBody is Player
        UnlockDestination();
    }

    private void GetPlayerController(Collider2D other)
    {
        //       if this                               do this                                                   else do this
        player = other.gameObject.CompareTag("Player")
            ? other.attachedRigidbody.GetComponent<PlayerController>()
            : null;
    }

    private void UnlockDestination()
    {
        isDestination = false;
    }
}
