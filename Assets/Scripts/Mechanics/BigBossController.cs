using UnityEngine;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Collider2D))]
    public class BigBossController : MyCharacterController
    {
        /// <summary> For development only. Used when I need to move the character and then put it back. </summary>
        private Vector3 bigBossDefaultPosition = new Vector3(-10.6068935f, -12.826f, -0.401916862f);
        public Bounds BoundsBigBoss => collider2dBigBoss.bounds;

        #region COLLIDER VARIABLES

        public Collider2D collider2dBigBoss;

        #endregion

        #region JUMP VARIABLES

        // For Scaring the Player
        internal bool scareJump;

        #endregion

        #region SCALLING VARIABLES

        // For scaling, get Bigger
        internal bool canBecomeBigger;
        readonly Vector3 finalScale = new Vector3(6, 6, 6);
        private const float duration = 4f;

        /// <summary> Used for the scaling calculation </summary>
        private float timer;

        /// <summary> Since Big Boss scales only the lower part, we move it's Y position at the same time. </summary>
        private Vector3 targetPosition;

        #endregion

        protected override void Awake()
        {
            collider2dBigBoss = GetComponent<Collider2D>();
            base.Awake();
        }

        protected override void Update()
        {
            if (canBecomeBigger) BecomeBigger();
            base.Update();
        }

        #region JUMP

        public override void Jump()
        {
            jumpState = JumpStatePlayer.PrepareToJump;
            MoveRight();
        }

        #endregion

        #region SCALING

        private void BecomeBigger()
        {
            var transform1 = transform;
            var currentPosition = transform1.position;
            var currentScale = transform1.localScale;

            if (targetPosition == new Vector3())
            {
                SetTargetPosition(currentPosition);
            }

            if (currentScale.y < finalScale.y)
            {
                MakeBigBossBigger(currentPosition, currentScale);
            }

            if (currentScale.y >= finalScale.y)
            {
                ResetScalingVariables();
            }
        }

        private void SetTargetPosition(Vector3 currentPosition)
        {
            var initialPosition = currentPosition;
            var newYPosition = initialPosition.y + 2f;
            targetPosition = new Vector3(initialPosition.x, newYPosition, initialPosition.z);
        }

        private void MakeBigBossBigger(Vector3 currentPosition, Vector3 currentScale)
        {
            timer += Time.deltaTime;
            float fractionOfJourney = timer / duration;

            // For Position
            transform.position = Vector3.Lerp(currentPosition, targetPosition,
                fractionOfJourney);

            // For Scale
            transform.localScale = Vector3.Lerp(currentScale, finalScale,
                fractionOfJourney);
        }

        private void ResetScalingVariables()
        {
            canBecomeBigger = false;
            timer = 0.0f;
            targetPosition = new Vector3();
        }

        #endregion
    }
}
