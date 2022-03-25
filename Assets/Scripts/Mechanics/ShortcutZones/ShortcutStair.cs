using Platformer.Mechanics;
using UnityEngine;

public class ShortcutStair : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        var rb = other.attachedRigidbody;
        if (rb == null) return;
        var player = rb.GetComponent<PlayerController>();
        if (player == null) return;
        Debug.Log($"hi");
        player.jumpAi.StartJumping();
    }
}
