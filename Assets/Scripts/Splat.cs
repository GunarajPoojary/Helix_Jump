using PrimeTween;
using UnityEngine;
using UnityEngine.Pool;

namespace HelixJump.Player
{
    public class Splat : MonoBehaviour
    {
        [SerializeField] private float _colorFadeDuration = 0.5f;
        [SerializeField] private Color _originalColor;
        [SerializeField] private Color _fadeColor;

        private ObjectPool<Splat> _pool;
        private SpriteRenderer _spriteRenderer;

        private void Awake() => _spriteRenderer = GetComponent<SpriteRenderer>();

        private void OnEnable() => FadeOut();

        public void Init(ObjectPool<Splat> pool) => _pool = pool;

        private void FadeOut()
        {
            _spriteRenderer.color = _originalColor;

            Tween.Color(
                _spriteRenderer,
                _fadeColor,
                _colorFadeDuration,
                Ease.Linear
            ).OnComplete(ReturnToPool);
        }

        private void ReturnToPool() => _pool.Release(this);
    }
}