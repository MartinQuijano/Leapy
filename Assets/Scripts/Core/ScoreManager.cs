using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int currentScore;
    private bool modified;

    public Text textScore;

    void Start()
    {
        currentScore = 0;
        modified = false;
        textScore.text = "score: 0";
    }

    void Update()
    {
        textScore.text = "score: " + currentScore;
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore = currentScore + scoreToAdd;
        modified = true;
    }

    public int GetScore()
    {
        return currentScore;
    }

    public bool IsScoreModified()
    {
        return modified;
    }

    public void UpdatedToLastCheck()
    {
        modified = false;
    }
}