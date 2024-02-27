using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] int rotationSpd = 200; // Speed of rotation

    // Update is called once per frame
    void Update()
    {
        // Get input from the horizontal axis
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Check if the game is active
        if (GameManager.Instance.gameIsActive)
        {
            // Rotate the object around the y-axis based on horizontal input
            // Time.deltaTime ensures smooth rotation across different frame rates
            transform.Rotate(Vector3.down * horizontalInput * Time.deltaTime * rotationSpd);
        }
    }
}
