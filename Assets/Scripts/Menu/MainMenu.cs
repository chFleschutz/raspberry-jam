using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI highScore;

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Start()
    {
        highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }
}
