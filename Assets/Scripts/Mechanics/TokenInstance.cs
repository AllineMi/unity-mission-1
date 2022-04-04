using System;
using UnityEngine;
using Platformer.Gameplay;
using Random = UnityEngine.Random;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class contains the data required for implementing token collection mechanics.
    /// It does not perform animation of the token, this is handled in a batch by the TokenController in the scene.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class TokenInstance : MonoBehaviour
    {
        public AudioClip collectAudio;

        [Tooltip("If true, animation will start at a random position in the sequence.")]
        public bool randomAnimationStartTime;

        [Tooltip("List of frames that make up the animation.")]
        public Sprite[] idleAnimation, collectedAnimation;

        internal Sprite[] sprites = Array.Empty<Sprite>();
        internal SpriteRenderer spriteRenderer;

        //unique index which is assigned by the TokenController in a scene.
        internal int index = -1;
        internal TokenController controller;

        //active frame in animation, updated by the controller.
        internal int frame;
        internal bool collected;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            if (randomAnimationStartTime)
            {
                frame = Random.Range(0, sprites.Length);
            }

            sprites = idleAnimation;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //only exectue OnPlayerEnter if the player collides with this token.
            var player = other.gameObject.GetComponent<PlayerController>();

            if (player == null) return;

            OnPlayerEnter(player);
        }

        void OnPlayerEnter(PlayerController player)
        {
            if (collected) return;

            // Disable the gameObject and remove it from the controller update list.
            frame = 0;
            sprites = collectedAnimation;

            if (controller != null)
            {
                collected = true;
            }

            // Send an event into the gameplay system to perform some behaviour.
            var ev = Schedule<PlayerTokenCollision>();
            ev.tokenInstance = this;
            ev.player = player;
        }

        public void PlayCollectAudio()
        {
            AudioSource.PlayClipAtPoint(collectAudio, transform.position);
        }
    }
}
