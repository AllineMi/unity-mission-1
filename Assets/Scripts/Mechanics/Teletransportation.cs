using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;

/// <summary>
/// This event is triggered when the player character enters a trigger with a VictoryZone component.
/// </summary>
/// <typeparam name="Teletransportation"></typeparam>
public class Teletransportation : Simulation.Event<Teletransportation>
{
    public TeleportPad destinationPad;
    private PlayerController playerController;
    public Collider2D collider2D;

    public override void Execute()
    {
        playerController = collider2D.gameObject.GetComponent<PlayerController>();
        
        TurnPlayerEast();
        
        LockDestination();
        
        DisablePlayerControl();
        
        var playerDestination = GetPlayerDestination();
        TeleportPlayer(playerDestination);
        
        EnablePlayerControl();

        UnlockDestination();
    }
    
    private void TurnPlayerEast()
    {
        playerController.spriteRenderer.flipX = false;
    }
    
    private void LockDestination()
    {
        destinationPad.isDestination = true;
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
    
    private void UnlockDestination()
    {
        destinationPad.isDestination = false;
    }
}