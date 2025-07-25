using UnityEngine;

namespace HelixJump.Data.Layer
{
    [System.Serializable]
    public struct PlatformsLayerData
    {
        public LayerMask safePlatformLayer; 
        public LayerMask forbiddenPlatformLayer; 
        public LayerMask scorePlatformLayer; 
        public LayerMask levelCompletionPlatformLayer; 
    }
}