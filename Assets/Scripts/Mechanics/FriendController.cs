using UnityEngine;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Collider2D))]
    public class FriendController : MyCharacterController
    {
        #region AUDIO

        public AudioClip jumpAudio;
        public AudioSource audioSource;

        #endregion

        #region BOUNDS

        public Bounds Bounds => collider2d.bounds;

        #endregion

        #region COLLIDER

        public Collider2D collider2d;

        #endregion

        protected override void Awake()
        {
            // AUDIO
            audioSource = GetComponent<AudioSource>();

            // COLLIDER
            collider2d = GetComponent<Collider2D>();

            base.Awake();
        }

        #region JUMP

        public override void Jump()
        {
            //throw new System.NotImplementedException();
        }

        #endregion
    }
}
