using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class ScoreManager : MonoBehaviour
{

    public TMP_Text CurrentScore;
    private int score;

    private void Start()
    {
        score = 0;
    }

    public void AddScore()
    {
        score += 1;
        CurrentScore.text = "Current Score: " + score.ToString();
    }

    public int getScore()
    {
        return score;
    }

}
