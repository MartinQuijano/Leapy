using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Frog frog;
    public GameObject gameOverScreen;
    public Text textScore;
    public ScoreManager scoreManager;

    public AudioClip deathClip;
    private AudioSource audioSource;
    private bool isGameOver;

    public static bool gameIsPaused;

    void Start()
    {
        Time.timeScale = 1;
        gameOverScreen.SetActive(false);
        textScore.text = "score: 0";
        audioSource = GetComponent<AudioSource>();
        isGameOver = false;
        gameIsPaused = false;
    }

    void Update()
    {
        if (!frog.isSafe() && !isGameOver)
        {
            isGameOver = true;
            audioSource.PlayOneShot(deathClip, 1f);
            textScore.text = "score: " + scoreManager.GetScore();
            CompareAndSetHighscore();
            gameOverScreen.SetActive(true);
            gameIsPaused = true;
            Time.timeScale = 0;
        }
    }

    private void CompareAndSetHighscore()
    {
        if (scoreManager.GetScore() > PlayerPrefs.GetInt("Score", 0))
        {
            PlayerPrefs.SetInt("Score", scoreManager.GetScore());
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
