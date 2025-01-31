using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public float timerDuration = 20f; // Duration of the timer in seconds
    private float remainingTime;
    private bool timerRunning = true;

    private TextMeshProUGUI timerText; // Reference to TextMeshProUGUI

    void Start()
    {
        // Get the TextMeshProUGUI component attached to the same GameObject
        timerText = GetComponent<TextMeshProUGUI>();

        // Initialize remaining time
        remainingTime = timerDuration;

        // Check if the TextMeshProUGUI component is found
        if (timerText == null)
        {
            Debug.LogError("TextMeshProUGUI component is missing on this GameObject!");
        }
    }

    void Update()
    {
        if (timerRunning)
        {
            // Decrease remaining time
            remainingTime -= Time.deltaTime;

            // Update the text display
            timerText.text = "Time Left: " + Mathf.Ceil(remainingTime).ToString() + "s";

            // Stop the timer and game when time runs out
            if (remainingTime <= 0)
            {
                remainingTime = 0;
                timerRunning = false;
                timerText.text = "Time's Up!";
                StopGame();
            }
        }
    }

    // Method to stop the game
    private void StopGame()
    {
        // Pause the game
        Time.timeScale = 0f;

        // Optional: Display a Game Over UI or Restart option
        Debug.Log("Game Over! Timer has ended.");
    }

    private void Addtime()
    {
        remainingTime += Time.deltaTime; 
    }
    public void AddTime(int time)
    {
        remainingTime += time; // Add 5 seconds to the remaining time
    }
}
