using UnityEngine;

namespace Platformer.Mechanics
{
    public class FriendController : MonoBehaviour
    {
        internal AnimationController control;
        internal Collider2D _collider;
        SpriteRenderer spriteRenderer;
        Animator animator;
        
        public Bounds Bounds => _collider.bounds;
        
        void Awake()
        {
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }
    }
}