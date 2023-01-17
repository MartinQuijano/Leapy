using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{

    public Text textHighscore;

    void Start()
    {
        int highscore = PlayerPrefs.GetInt("Score", 0);
        textHighscore.text = "highscore: " + highscore;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
