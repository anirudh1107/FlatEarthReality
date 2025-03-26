using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [Header("Time Settings")]
    [Tooltip("Start hour in the game (e.g., 9 for 09:00).")]
    public int startHour = 9;
    [Tooltip("End hour in the game (e.g., 17 for 17:00).")]
    public int endHour = 17;

    // Total in-game hours the level lasts.
    private int totalGameHours;
    // Duration of one in-game hour in real seconds.
    private float realSecondsPerHour = 60f;
    // Accumulator for real time.
    private float timer;

    // Current in-game hour.
    private int currentHour;
    // Current in-game minute (optional, if you want finer resolution).
    private int currentMinute;

    [Header("UI Settings")]
    [Tooltip("UI Text component for displaying the game time.")]
    public TMP_Text timerText;

    // Event triggered when time reaches endHour.
    public event Action OnTimeUp;

    private bool levelEnded = false;

    private void Start()
    {
        totalGameHours = endHour - startHour;
        currentHour = startHour;
        currentMinute = 0;
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (levelEnded) return;

        // Increase timer by the elapsed real time.
        timer += Time.deltaTime;

        // Check if one in-game hour has passed.
        if (timer >= realSecondsPerHour)
        {
            // Deduct the time for one in-game hour.
            timer -= realSecondsPerHour;
            currentHour++;

            // Optionally calculate minutes if you want to show minutes passing
            currentMinute = Mathf.FloorToInt((timer / realSecondsPerHour) * 60f);

            UpdateTimerDisplay();

            // End level if time reaches or exceeds the end hour.
            if (currentHour >= endHour)
            {
                levelEnded = true;
                // Trigger any end level functionality.
                OnTimeUp?.Invoke();
                Debug.Log("Time's up! Level ended.");
            }
        }
        else
        {
            // Update minutes if desired.
            currentMinute = Mathf.FloorToInt((timer / realSecondsPerHour) * 60f);
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        // Format the time as HH:MM (e.g., 09:00, 09:30, etc.)
        string hourText = currentHour.ToString("D2");
        string minuteText = currentMinute.ToString("D2");
        if (timerText != null)
        {
            timerText.text = $"{hourText}:{minuteText}";
        }
    }
}
