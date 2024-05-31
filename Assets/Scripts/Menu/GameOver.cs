using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : GameEventListener
{
    [SerializeField] private GameEvent PlayerDiedEvent;
    [SerializeField] private GameObject gameOverMenu;

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public override void OnInvoke()
    {
        gameOverMenu.SetActive(true);
        Debug.Log("GameOver: Player Died");
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
