using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI object for displaying the score

    // Update is called once per frame
    void Update()
    {
        // Update the score text to display the current score from the GameManager
        scoreText.text = "Score : " + GameManager.Instance.score;
    }
}
