using System;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

public class TeleportPad : MonoBehaviour
{
    //[RequiredInterface(TeleportPad)]
    //[RequiredMember]
    public Rigidbody2D destinationPad;
    
    [Tooltip("Check it if you want this pad to be used only as a destination and never .the only one enabled.")]
    public bool disableDepartures = false;
    [HideInInspector]
    public PlayerController player;
    
    private bool isDestination;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        GetPlayerController(collider2D);

        if (isDestination || player == null || disableDepartures) return; // Will leave if Pad is the destination

        //Only trigger if the triggerBody is Player
        LockDestination();
        var ev = Schedule<Teletransportation>();
        ev.destinationPad = destinationPad;
        ev.playerController = player;
    }
    
    protected void Awake()
    {
        TeleportPad destinationTeleportPad;
        try
        {
            destinationTeleportPad = destinationPad.GetComponent<TeleportPad>();
        }
        catch (Exception e)
        {
            //InspectorHighlight. ("Inspector", "destinationPad");
            
            Console.WriteLine(e);
            throw;
        }
        //
        // //TODO organize this code. GetComponents<TeleportPad>() is getting all pads beside the first one...I think
        // // If there's only one Teleport Pad in game
        // var numberOfTeleportPadsInGame = GetComponents<TeleportPad>();
        // Debug.Log($"pad number {numberOfTeleportPadsInGame.Length}");
        //
        // foreach (var pad in numberOfTeleportPadsInGame)
        // {
        //     Debug.Log($"{pad.name}");
        // }
        //
        // if (numberOfTeleportPadsInGame.Length < 2)
        // {
        //     Debug.LogError($"You need another TransportPad for Teletransportation");
        //     return;
        // }
        //
        // // Check how many pads are in game, if only one, display message below
        // if (destinationTeleportPad == null)
        //     Debug.LogError($"TransportPad -> {this.name} <- does not have a destination pad.");
        //
        //
        // // If isOneWayTrip and destinationPad's field "Destination Pad" is empty
        // // In other words, if you do not want the destination pad to work as a teleporter
        // if (disableDepartures && destinationTeleportPad.destinationPad == null)
        //     destinationTeleportPad.GetComponent<BoxCollider2D>().enabled = false;
        //
        // // If isOneWayTrip but destinationPad's field "Destination Pad" is not empty
        // if(disableDepartures && destinationTeleportPad != null)
        //     Debug.LogError($"\"Is One Way Trip\" box is checked but the \"Destination Pad\" for \"{destinationTeleportPad.name}\" is not empty.\n" +
        //                    $"Uncheck the box \"Is One Way Trip\" if you want the \"{destinationTeleportPad.name}\" to work as a teleporter.");
        //
        // if (destinationTeleportPad.destinationPad == null)
        //     Debug.LogError($"TransportPad -> {destinationTeleportPad.name} <- does not have a destination pad.\n" +
        //                    $"Check the box \"Is One Way Only\" if you do not want to set a destination for this pad.");
        
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        GetPlayerController(collider2D);

        if (!isDestination) return; // Will leave if Pad is not the destination
        if (player == null) return;

        //Only trigger if the triggerBody is Player
        UnlockDestination();
    }

    private void GetPlayerController(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) //player = other.gameObject.CompareTag("Player") ? other.attachedRigidbody.GetComponent<PlayerController>() : null;
            player = other.attachedRigidbody.GetComponent<PlayerController>();
        else
            player = null;
    }

    private void LockDestination()
    {
        destinationPad.GetComponent<TeleportPad>().isDestination = true;
    }

    private void UnlockDestination()
    {
        isDestination = false;
    }
    /**
     * --------------------------------------------------------------------------------------------------------------
     * TODO how this code works
     * Got from 
     * 
     */

    // [SerializeField]
    // private List<UnmaskedView> m_UnmaskedViews = new List<UnmaskedView>();
    
    
    // /// <summary>
    // /// The masking settings for this paragraph.
    // /// </summary>
    // public MaskingSettings maskingSettings
    // {
    //     get
    //     {
    //         return m_MaskingSettings;
    //     }
    // }
    //
    // [SerializeField]
    // MaskingSettings m_MaskingSettings = new MaskingSettings();
    //
    // public MaskingSettings currentMaskingSettings
    // {
    //     get
    //     {
    //         MaskingSettings result = null;
    //         for (int i = 0, count = m_Paragraphs.count; i < count; ++i)
    //         {
    //             if (!m_Paragraphs[i].maskingSettings.enabled) { continue; }
    //
    //             result = m_Paragraphs[i].maskingSettings;
    //             if (!m_Paragraphs[i].completed)
    //                 break;
    //         }
    //         return result;
    //     }
    // }
    //
    // public const string k_EnabledPath = "m_MaskingEnabled";
    // public const string k_UnmaskedViewsPath = "m_UnmaskedViews";

    // public bool enabled
    // {
    //     get
    //     {
    //         return m_MaskingEnabled;
    //     }
    //     set
    //     {
    //         m_MaskingEnabled = value;
    //     }
    // }
    //
    // [SerializeField, FormerlySerializedAs("m_Enabled")]
    // private bool m_MaskingEnabled;
    //
    // public IEnumerable<UnmaskedView> unmaskedViews
    // {
    //     get
    //     {
    //         return m_UnmaskedViews;
    //     }
    // }
    
    
    
    //
    // public void SetUnmaskedViews(IEnumerable<UnmaskedView> unmaskedViews)
    // {
    //     m_UnmaskedViews.Clear();
    //     if (unmaskedViews != null)
    //         m_UnmaskedViews.AddRange(unmaskedViews);
    // }
}