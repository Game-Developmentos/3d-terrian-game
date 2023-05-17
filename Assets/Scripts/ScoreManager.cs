using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class ScoreManager : MonoBehaviour
{

    public TMP_Text CurrentScore;
    private int score = 0;

    // Adds one to the score and updates the score display.
    public void AddScore()
    {
        score += 1;
        CurrentScore.text = "Current Score: " + score.ToString();
    }

    // Returns the current score value.
    public int getScore()
    {
        return score;
    }

}
