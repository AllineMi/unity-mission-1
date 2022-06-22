using UnityEngine;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Collider2D))]
    public class FriendController : BaseController
    {
        #region AUDIO

        public AudioClip jumpAudio;
        public AudioSource audioSource;

        #endregion

        #region BOUNDS

        public Bounds Bounds => boxCollider2D.bounds;

        #endregion

        #region COLLIDER

        public BoxCollider2D boxCollider2D;

        #endregion

        protected override void Awake()
        {
            // AUDIO
            audioSource = GetComponent<AudioSource>();

            // COLLIDER
            boxCollider2D = GetComponent<BoxCollider2D>();

            base.Awake();
        }

        protected override void Update()
        {
            if (jumpState == JumpState.Landed)
            {
                StopMoving();
            }

            base.Update();
        }

        #region AUDIO

        internal void PlayJumpAudio()
        {
            audioSource.PlayOneShot(jumpAudio);
        }

        #endregion
    }
}
