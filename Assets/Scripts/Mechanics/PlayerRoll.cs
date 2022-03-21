// using Platformer.Core;
// using Platformer.Mechanics;
// using Platformer.Model;
// using UnityEngine;
//
// public class PlayerRoll : MonoBehaviour
// {
//     public PlayerController player;
//     private Rigidbody2D playerRigidBody;
//     PlatformerModel model = Simulation.GetModel<PlatformerModel>();
//     public float toRotate = 10f;
//     private float rotationSpeed = 1f;
//
//     // Start is called before the first frame update
//     void Start()
//     {
//         player = model.player;
//         playerRigidBody = player.animator.GetComponent<Rigidbody2D>();
//         if(player == null) Debug.Log($"player null");
//         if (player.token.allTokensCollected)
//         {
//             //playerRigidBody
//             // if (player.jumpState == PlayerController.JumpState.Grounded)
//             // {
//             //     playerRigidBody.bodyType = RigidbodyType2D.Static;
//             // }
//         }
//
//         Debug.Log($"");
//     }
//
//     // Update is called once per frame
//     void Update()
//     {
//         if (player.token.allTokensCollected)
//         {
//             // toRotate += Time.deltaTime * rotationSpeed;
//             player.transform.Rotate(0f,0f,toRotate);
//             playerRigidBody.rotation = toRotate;
//             //player.transform.Rotate(0f,0f,toRotate, Space.Self);
//             //player.transform.rotation = Quaternion.Euler(0, 0, toRotate);
//         }
//     }
// }
