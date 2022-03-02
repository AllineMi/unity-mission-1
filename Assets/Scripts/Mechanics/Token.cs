using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// When player collects more than 10 tokens
    /// player sprite should get rounder
    /// </summary>
    public class Token : MonoBehaviour
    {
        // public PlayerController player;
        // public TokenController token;
        // public int maxToken;
        // public int currentToken;
        //
        // void Awake()
        // {
        //     maxToken = GetComponent<TokenController>().tokens.Length;
        // }
        //
        // public void Increment()
        // {
        //     currentToken = Mathf.Clamp(currentToken + 1, 0, maxToken);
        //     BecomeBigger();
        //     
        // }
        //
        // public void BecomeBigger()
        // {
        //     if (currentToken == 1)
        //     {
        //         var a = player.animator.avatar.name;
        //         Debug.Log($"Avatar: {a}");
        //     }
        // }
    }
}