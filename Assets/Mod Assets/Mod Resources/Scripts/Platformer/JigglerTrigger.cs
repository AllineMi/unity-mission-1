using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Jiggler))]
public class JigglerTrigger : MonoBehaviour
{
    //TODO to act only if a specific object enters the trigger
    private static float jPower;

    protected virtual void Awake()
    {
        jPower = GetComponent<Jiggler>().power;
        GetComponent<Jiggler>().power = .0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<Jiggler>().power = jPower;
    }
}
