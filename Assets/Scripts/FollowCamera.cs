using UnityEngine;

namespace HelixJump.Camera
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _smoothSpeed = 5f;
        [SerializeField] private float _yOffset = 5f;

        private float _initialX;
        private float _initialZ;

        private float _lowestY;

        private void Start()
        {
            _initialX = transform.position.x;
            _initialZ = transform.position.z;

            if (_target)
                _lowestY = _target.position.y; 
        }

        private void LateUpdate()
        {
            if (!_target) return;

            if (_target.position.y < _lowestY)
                _lowestY = _target.position.y;

            Vector3 desiredPosition = new(
                _initialX,
                _lowestY + _yOffset,
                _initialZ
            );

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);

            transform.position = smoothedPosition;
        }
    }
}