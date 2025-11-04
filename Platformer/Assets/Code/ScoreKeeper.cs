using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip positiveScoreSound;
    public AudioClip negativeScoreSound;
    
    private static float score;
    private static Text scoreText;
    private static AudioSource audioSource;
    private static AudioClip positiveSoundStatic;
    private static AudioClip negativeSoundStatic;

    void Start()
    {
        scoreText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
        
        // Store references to audio clips in static variables
        positiveSoundStatic = positiveScoreSound;
        negativeSoundStatic = negativeScoreSound;
        
        UpdateText();
    }

    public static void AddToScore(float points)
    {
        score += points;
        UpdateText();
        
        // Play appropriate sound
        if (audioSource != null)
        {
            if (points > 0 && positiveSoundStatic != null)
            {
                audioSource.PlayOneShot(positiveSoundStatic);
            }
            else if (points < 0 && negativeSoundStatic != null)
            {
                audioSource.PlayOneShot(negativeSoundStatic);
            }
        }
    }

    private static void UpdateText()
    {
        if (scoreText != null)
        {
            scoreText.text = String.Format("Score: {0}", score);
        }
    }
    
    public static float GetScore()
    {
        return score;
    }
    
    public static void ResetScore()
    {
        score = 0;
        UpdateText();
    }
}