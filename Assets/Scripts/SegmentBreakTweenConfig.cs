using PrimeTween;
using UnityEngine;

namespace HelixJump
{
    [CreateAssetMenu(menuName = "Custom/Segment Break Tween")]
    public class SegmentBreakTweenConfig : ScriptableObject
    {
        [field:SerializeField] public float TweenDuration = 0.5f;
        [field:SerializeField] public Ease EaseType = Ease.InSine;
    }
}