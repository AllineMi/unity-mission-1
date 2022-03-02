using System;
using Platformer.Core;
using Platformer.Gameplay;
using UnityEngine;
using Platformer.Mechanics;
using Platformer.Model;

public class Transport : MonoBehaviour
{
    public GameObject destinationPad;
    PlatformerModel model = Simulation.GetModel<PlatformerModel>();
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // About the Destination Pad
        var destinationPosition = destinationPad.transform.position;
        
        // About the Player
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        model.player.spriteRenderer.flipX = false; // Player will always face east
        var playerBody = other.attachedRigidbody;
        var playerController = playerBody.GetComponent<PlayerController>();
        playerController.controlEnabled = false; // Disable player control

        playerController.Teleport(destinationPosition);
        
        Simulation.Schedule<EnablePlayerInput>(1f); // Enables player control
    }
}