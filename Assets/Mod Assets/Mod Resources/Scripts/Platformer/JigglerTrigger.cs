using UnityEngine;
using Platformer.Mechanics;

[RequireComponent(typeof(BoxCollider2D), typeof(Jiggler))]
public class JigglerTrigger : BasePlayerColliderTrigger
{
    private static float jPower;

    private void Awake()
    {
        jPower = GetComponent<Jiggler>().power;
        GetComponent<Jiggler>().power = 0f;
    }

    protected override void DoEnterTriggerAction()
    {
        GetComponent<Jiggler>().power = jPower;
    }

    protected override void DoExitTriggerAction()
    {
        // throw new System.NotImplementedException();
    }
}
