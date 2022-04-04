using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
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

        #region ANIMATION

        internal void PlayVictoryRunAnimation()
        {
            animator.SetTrigger("victoryRun");
        }

        internal void PlayVictoryAnimation()
        {
            animator.SetTrigger("victory");
        }

        #endregion

        #region JUMP

        public override void Jump()
        {
            //throw new System.NotImplementedException();
        }

        #endregion
    }
}
