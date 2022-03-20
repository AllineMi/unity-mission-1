using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;

/// <summary>
/// This event is triggered when the player character enters a trigger with a TeleportPad component.
/// </summary>
/// <typeparam name="Teletransportation"></typeparam>
public class Teletransportation : Simulation.Event<Teletransportation>
{
    internal Rigidbody2D destinationPad;
    internal PlayerController playerController;

    public override void Execute()
    {
        LockDestination();
        
        DisablePlayerControl();
        
        TurnPlayerEast();

        var playerDestination = GetPlayerDestination();
        TeleportPlayer(playerDestination);
        
        EnablePlayerControl();
    }
    
    private void LockDestination()
    {
        destinationPad.GetComponent<TeleportPad>().isDestination = true;
    }
    
    private void TurnPlayerEast()
    {
        playerController.spriteRenderer.flipX = false;
    }
    
    private void DisablePlayerControl()
    {
        Simulation.Schedule<DisablePlayerInput>(); // Disables player control
    }
    
    private Vector3 GetPlayerDestination()
    {
        return destinationPad.transform.position;
    }
    
    private void TeleportPlayer(Vector3 playerDestination)
    {
        playerController.Teleport(playerDestination);
    }
    
    private void EnablePlayerControl()
    {
        Simulation.Schedule<EnablePlayerInput>(1f); // Enables player control
    }
}