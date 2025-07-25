using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HelixJump
{
    [RequireComponent(typeof(Image))]
    public class TweenButton : Button
    {
        [SerializeField] private ButtonTweenConfig _buttonTweenConfig;
        private Vector3 _originalScale;

        protected override void Awake()
        {
            base.Awake();
            _originalScale = transform.localScale;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            image.color = _buttonTweenConfig.PressedColor;
            _buttonTweenConfig.PlayPressedSound();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            Tween.Scale(transform, _buttonTweenConfig.PressedScale, _buttonTweenConfig.TweenDuration, _buttonTweenConfig.PressedEase);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            Tween.Scale(transform, _originalScale, _buttonTweenConfig.TweenDuration, _buttonTweenConfig.PressedEase);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            image.color = _buttonTweenConfig.HoverColor;
            _buttonTweenConfig.PlayHoverSound();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            image.color = _buttonTweenConfig.NormalColor;
        }
    }
}