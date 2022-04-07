using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary> A simple controller for enemies. Provides movement control over a patrol path. </summary>
    [RequireComponent(typeof(Collider2D))]
    public class EnemyController : BaseController
    {
        #region AUDIO VARIABLES

        // AUDIO SETTINGS
        public AudioClip ouch;
        internal AudioSource audioeEnemy;

        #endregion

        #region BOUNDS VARIABLES

        public Bounds BoundsEnemy => colliderEnemy.bounds;

        #endregion

        #region COLLIDER VARIABLES

        internal Collider2D colliderEnemy;

        #endregion

        #region PATH VARIABLES

        // PATH SETTINGS
        public PatrolPath path;
        PatrolPath.Mover mover;

        #endregion

        protected override void Awake()
        {
            // AUDIO
            audioeEnemy = GetComponent<AudioSource>();

            // COLLIDER
            colliderEnemy = GetComponent<Collider2D>();

            base.Awake();
        }

        protected override void Update()
        {
            if (path == null) return;

            if (stopMoving)
            {
                move.x = 0f;
            }
            else
            {
                if (mover == null)
                {
                    mover = path.CreateMover(maxSpeed * 0.5f);
                }

                move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
            }

            base.Update();
        }

        #region AUDIO

        internal void PlayHurtAudio()
        {
            audioeEnemy.PlayOneShot(ouch);
        }

        #endregion

        #region COLLIDER

        void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player == null) return;

            var ev = Schedule<PlayerEnemyCollision>();
            ev.player = player;
            ev.enemy = this;
        }

        #endregion

        #region JUMP

        public override void Jump()
        {
            // throw new System.NotImplementedException();
        }

        #endregion

        #region MOVEMENT

        internal override void StopMoving()
        {
            stopMoving = true;
        }

        #endregion
    }
}
