using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target to follow ball
    public float smoothing = 5f; // The smoothing factor for the camera movement

    Vector3 offset; // The initial offset between the camera and the target

    void Start()
    {
        // Calculate the initial offset between the camera and the target
        offset = (transform.position - new Vector3(0,1,0)) - target.position;
    }

    void FixedUpdate()
    {
        // Calculate the target position with smoothing
        Vector3 targetPosition = target.position + offset;
        // Smoothly interpolate between the current camera position and the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.fixedDeltaTime);
    }
}
