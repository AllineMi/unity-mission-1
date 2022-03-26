using Platformer.Mechanics;
using UnityEngine;

public class ShortcutStair : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        var rb = other.attachedRigidbody;
        if (rb == null) return;
        var player = rb.GetComponent<PlayerController>();
        if (player == null) return;
        Debug.Log($"hi");
        //player.jumpAi.StartJumping();
    }
}
