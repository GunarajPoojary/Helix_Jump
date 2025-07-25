using PrimeTween;
using UnityEngine;

namespace HelixJump.Player
{
    public class Segment : MonoBehaviour
    {
        [SerializeField] private SegmentBreakTweenConfig _segmentBreakTweenConfig;
        [SerializeField] private GameObject[] _platforms = new GameObject[] { };
        private bool _hasBurstOut = false;

        private MeshCollider[] _meshColliders;

        private void Awake()
        {
            _meshColliders = new MeshCollider[_platforms.Length];

            for (int i = 0; i < _platforms.Length; i++)
                _meshColliders[i] = _platforms[i].GetComponent<MeshCollider>();
        }

        private void OnTriggerEnter(Collider col) => BurstOut();

        private void BurstOut()
        {
            if (_hasBurstOut) return;

            _hasBurstOut = true;

            for (int i = 0; i < _platforms.Length; i++)
            {
                Transform platformTransform = _platforms[i].transform;

                if (_meshColliders[i] != null)
                    _meshColliders[i].enabled = false;

                Tween.LocalPosition(
                    platformTransform,
                    platformTransform.localRotation * Vector3.forward * -2f,
                    _segmentBreakTweenConfig.TweenDuration * 0.1f,
                    _segmentBreakTweenConfig.EaseType
                ).OnComplete(() => Drop(platformTransform));
            }
        }

        private void Drop(Transform platformTransform)
        {
            Tween.LocalPositionY(
                    platformTransform,
                    -50f,
                    _segmentBreakTweenConfig.TweenDuration,
                    _segmentBreakTweenConfig.EaseType
                ).OnComplete(() => platformTransform.gameObject.SetActive(false));
        }
    }
}