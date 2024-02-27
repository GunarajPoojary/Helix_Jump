using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance of the GameManager.
    public static GameManager Instance;

    // TextMeshProUGUI objects for displaying win and lose messages.
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;

    public int score = 0; // Current score
    public bool gameIsActive = true; // Flag to indicate if the game is active

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        // Ensure that there is only one instance of GameManager.
        if (Instance == null)
            Instance = this;
    }

    // Method to display win message for Player 1.
    public void Win()
    {
        // Activate the winText object.
        winText.gameObject.SetActive(true);
    }

    // Method to display lose message for Player 1.
    public void Lose()
    {
        // Activate the loseText object.
        loseText.gameObject.SetActive(true);
    }
}
