using UnityEngine;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Collider2D))]
    public class BigBossController : MyCharacterController
    {
        public Vector3 defaultPosition = new Vector3(-10.6068935f, -12.643858f, -0.401916862f);
        public Collider2D collider2d;
        public Bounds Bounds => collider2d.bounds;

        // For scaling
        public bool canBecomeBigger;
        private float timer;

        private const float duration = 20f;

        // Used when we want to scale the object
        Vector3 targetPosition;

        protected override void Awake()
        {
            collider2d = GetComponent<Collider2D>();
            base.Awake();
        }

        protected override void Update()
        {
            if (canBecomeBigger) BecomeBigger();
            base.Update();
        }

        private void BecomeBigger()
        {
            // Position
            var transform1 = transform;
            var currentPosition = transform1.position;
            var currentScale = transform1.localScale;

            if (targetPosition == new Vector3())
            {
                var initialPosition = currentPosition;
                var newYPosition = initialPosition.y + 2f;
                targetPosition = new Vector3(initialPosition.x, newYPosition, initialPosition.z);
            }

            if (currentScale.y < 6f)
            {
                var finalScale = new Vector3(6, 6, 6);

                timer += Time.deltaTime;
                float fractionOfJourney = timer / duration;

                // For Position
                transform.position = Vector3.Lerp(currentPosition, targetPosition,
                    fractionOfJourney);

                // For Scale
                transform.localScale = Vector3.Lerp(currentScale, finalScale,
                    fractionOfJourney);
            }
            else
            {
                canBecomeBigger = false;
                timer = 0.0f;
                targetPosition = new Vector3();
            }
        }
    }
}
