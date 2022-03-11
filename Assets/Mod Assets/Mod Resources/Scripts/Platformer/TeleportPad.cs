using UnityEngine;
using static Platformer.Core.Simulation;

public class TeleportPad : MonoBehaviour
{
    public TeleportPad _destinationPad;
    public bool isDestination;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDestination) return;
        
        // var ev = Schedule<Teletransportation>();
        var ev = Schedule<Teletransportation>();
        ev.destinationPad = this;
        ev.collider2D = other;
    }
}