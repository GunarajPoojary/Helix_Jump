using HelixJump.Data.Audio;
using HelixJump.Data.Layer;
using HelixJump.Events;
using PrimeTween;
using UnityEngine;
using UnityEngine.Pool;

namespace HelixJump.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class Ball : MonoBehaviour
    {
        [Header("Bounce Settings")]
        [SerializeField] private GameEvents _gameEvents;
        [SerializeField] private float _bounceForce = 6f;
        [SerializeField] private float _splatYOffset = 0.47f;
        [SerializeField] private float _squashDuration = 0.1f;
        [SerializeField] private Vector3 _squashScale = new(1.4f, 0.8f, 1.4f);

        [SerializeField] private PlatformsLayerData _platformsLayerData;

        [Header("Effects")]
        [SerializeField] private Splat _splatPrefab;
        [SerializeField] private BallAudioData _audioData;

        private ObjectPool<Splat> _splatPool;
        private Rigidbody _rb;
        private Transform _splatContainer;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();

            _splatContainer = new GameObject("SplatContainer").transform;

            _splatPool = new ObjectPool<Splat>(
            createFunc: CreateSplat,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            collectionCheck: false,
            defaultCapacity: 5,
            maxSize: 10);
        }

        private void OnCollisionEnter(Collision col)
        {
            Vector3 contactPoint = col.contacts[0].point;
            contactPoint.y = col.transform.position.y + _splatYOffset;

            Splat splat = _splatPool.Get();
            splat.transform.SetPositionAndRotation(contactPoint, Quaternion.Euler(Vector3.right * 90f));

            splat.transform.SetParent(col.gameObject.transform);

            if (((1 << col.gameObject.layer) & _platformsLayerData.safePlatformLayer) != 0)
            {
                Bounce();
            }
            else if (((1 << col.gameObject.layer) & _platformsLayerData.forbiddenPlatformLayer) != 0)
            {
                Stuck();
            }
            else if (((1 << col.gameObject.layer) & _platformsLayerData.levelCompletionPlatformLayer) != 0)
            {
                _gameEvents.GameCompleteEvent.RaiseEvent();
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            if (((1 << col.gameObject.layer) & _platformsLayerData.scorePlatformLayer) != 0)
            {
                _gameEvents.ScoreEvent.RaiseEvent();

                _gameEvents.PlayOneShotAudioEvent.RaiseEvent(_audioData.breakAudio);
            }
        }

        private Splat CreateSplat()
        {
            Splat splat = Instantiate(_splatPrefab, transform);
            splat.transform.SetParent(_splatContainer);
            splat.gameObject.SetActive(false);
            splat.Init(_splatPool);
            return splat;
        }

        private void OnGet(Splat splat) => splat.gameObject.SetActive(true);

        private void OnRelease(Splat splat)
        {
            splat.transform.SetParent(_splatContainer);
            splat.gameObject.SetActive(false);
        }

        private void Bounce()
        {
            Vector3 velocity = _rb.linearVelocity;
            velocity.y = _bounceForce;
            _rb.linearVelocity = velocity;

            _gameEvents.PlayOneShotAudioEvent.RaiseEvent(_audioData.bounceAudio);
            _gameEvents.BounceEvent.RaiseEvent();

            Squash();
        }

        private void Squash()
        {
            // Do squash & stretch
            Tween.Scale(
                transform,
                _squashScale,
                _squashDuration
            ).OnComplete(() =>
            {
                // Restore scale
                Tween.Scale(
                    transform,
                    Vector3.one,
                    _squashDuration
                );
            });
        }

        private void Stuck()
        {
            _rb.isKinematic = true;

            _gameEvents.PlayOneShotAudioEvent.RaiseEvent(_audioData.stickAudio);

            _gameEvents.LoseGameEvent.RaiseEvent();
        }
    }
}