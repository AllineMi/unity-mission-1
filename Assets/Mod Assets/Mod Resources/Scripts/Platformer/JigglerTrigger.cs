using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D), typeof(Jiggler))]
public class JigglerTrigger : MonoBehaviour
{
    private static float jPower;
    
    protected virtual void Awake()
    {
        jPower = GetComponent<Jiggler>().power;
        GetComponent<Jiggler>().power = .0f;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        var hitRb = other.attachedRigidbody;
        GetComponent<Jiggler>().power = jPower;
    }
}