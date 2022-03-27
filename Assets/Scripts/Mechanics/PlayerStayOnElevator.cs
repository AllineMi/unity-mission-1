using Mechanics;
using Platformer.Mechanics;
using UnityEngine;

public class PlayerStayOnElevator : MonoBehaviour
{
    public Elevator elevator;
    public PlayerController player;

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Elevator"))Debug.Log($"other: {other.name}");

        var rb = other.attachedRigidbody;
        if (rb == null) return;

        elevator = rb.GetComponent<Elevator>();
        Debug.Log($"elevator == null: {elevator == null}");
        if (elevator == null) return;
    }
}
