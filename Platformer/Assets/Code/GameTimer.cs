using System;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float gameTimeInSeconds = 60f;
    
    [Header("UI References")]
    public Text timerText;
    
    private float timeRemaining;
    private bool timerRunning = true;
    
    void Start()
    {
        timeRemaining = gameTimeInSeconds;
        UpdateTimerDisplay();
    }
    
    void Update()
    {
        if (timerRunning)
        {
            timeRemaining -= Time.deltaTime;
            
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerRunning = false;
            }
            
            UpdateTimerDisplay();
        }
        
        // check for Escape key to end game early
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            timerRunning = false;
        }
    }
    
    void UpdateTimerDisplay()
    {
        // format as minutes:seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = String.Format("Time: {0:0}:{1:00}", minutes, seconds);
    }
    
    public bool IsGameRunning()
    {
        return timerRunning;
    }
}