using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float bounceForce = 180; // The force to apply when the ball bounces

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the ball
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.linearVelocity = Vector3.up * bounceForce * Time.deltaTime; // Apply upward force when the ball collides

        if (collision.gameObject.CompareTag("Sticky"))
        {
            // If the ball collides with a "Sticky" object, trigger the Lose method in GameManager
            GameManager.Instance.Lose();
            GameManager.Instance.gameIsActive = false; // Set the game to inactive
            rb.isKinematic = true; // Make the Rigidbody kinematic to stop physics interactions
        }
        else if (collision.gameObject.CompareTag("End Slice"))
        {
            // If the ball collides with an "End Slice" object, trigger the Win method in GameManager
            GameManager.Instance.Win();
            GameManager.Instance.gameIsActive = false; // Set the game to inactive
            rb.isKinematic = true; // Make the Rigidbody kinematic to stop physics interactions
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sensor"))
        {
            // If the ball exits a "Sensor" trigger, increase the score by 5
            GameManager.Instance.score += 5;
        }
    }
}
