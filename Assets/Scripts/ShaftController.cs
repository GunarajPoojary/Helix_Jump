using HelixJump.Events;
using UnityEngine;

namespace HelixJump.Player
{
    public class ShaftController : MonoBehaviour
    {
        [SerializeField] private GameEvents _gameEvents;
        [SerializeField] private float _rotationSpeed = 100f;
        [SerializeField] private float _touchSensitivity = 2f;
        [SerializeField] private bool _invertRotation = false;

        private Vector2 _lastInputPosition;
        private readonly float _smoothingFactor = 0.8f;
        private float _targetRotationSpeed;
        private bool _isInputEnabled;

        public float CurrentRotationVelocity { get; private set; }

        private void OnEnable() => _gameEvents.GameActiveEvent.OnEventRaised += ToggleInput;
        private void OnDisable()=> _gameEvents.GameActiveEvent.OnEventRaised -= ToggleInput;

        private void Update()
        {
            if (_isInputEnabled)
            {
                HandleInput();
                ApplyRotation();
            }
        }

        private void ToggleInput(bool toggle) => _isInputEnabled = toggle;

        private void HandleInput()
        {
            float rotationInput = 0f;

#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
            rotationInput += GetKeyboardInput();
            rotationInput += GetMouseInput();
#elif UNITY_ANDROID || UNITY_IOS
            rotationInput += GetTouchInput();
#endif

            if (_invertRotation)
                rotationInput = -rotationInput;

            _targetRotationSpeed = rotationInput * _rotationSpeed;
            CurrentRotationVelocity = Mathf.Lerp(CurrentRotationVelocity, _targetRotationSpeed, _smoothingFactor);
        }

        private float GetKeyboardInput()
        {
            float input = 0f;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                input -= 1f;

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                input += 1f;

            return input;
        }

        private float GetMouseInput()
        {
            float input = 0f;

            if (Input.GetMouseButtonDown(0))
            {
                _lastInputPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector2 currentPosition = Input.mousePosition;
                float deltaX = currentPosition.x - _lastInputPosition.x;

                input = deltaX / Screen.width * _touchSensitivity;
                _lastInputPosition = currentPosition;
            }

            return input;
        }

        private float GetTouchInput()
        {
            float input = 0f;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _lastInputPosition = touch.position;
                        break;

                    case TouchPhase.Moved:
                        Vector2 currentPosition = touch.position;
                        float deltaX = currentPosition.x - _lastInputPosition.x;

                        input = (deltaX / Screen.width) * _touchSensitivity;
                        _lastInputPosition = currentPosition;
                        break;
                }
            }

            return input;
        }

        private void ApplyRotation()
        {
            if (Mathf.Abs(CurrentRotationVelocity) > 0.01f)
            {
                float rotationAmount = CurrentRotationVelocity * Time.deltaTime;
                transform.Rotate(0, rotationAmount, 0, Space.World);
            }
        }
    }
}