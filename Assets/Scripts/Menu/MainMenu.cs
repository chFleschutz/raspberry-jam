using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI highScore;
    [SerializeField] private Texture2D cursorTexture;

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
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}
