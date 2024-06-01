using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour, IGameEventListener
{
    [SerializeField] private GameEvent PlayerDiedEvent;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Scoreboard scoreboard;
    [SerializeField] private TMPro.TextMeshProUGUI highscore;
    [SerializeField] private TMPro.TextMeshProUGUI score;

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OnInvoke()
    {
        Debug.Log("GameOver: Player Died");
        
        highscore.text = "High Score: " + scoreboard.Highscore;
        score.text = "Score: " + scoreboard.Score;
        gameOverMenu.SetActive(true);
    }

    void Start()
    {
        gameOverMenu.SetActive(false);
        PlayerDiedEvent.RegisterListener(this);
    }

    void OnDestroy()
    {
        PlayerDiedEvent.UnregisterListener(this);
    }
}
