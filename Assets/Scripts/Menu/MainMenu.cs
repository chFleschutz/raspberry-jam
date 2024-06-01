using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("High Score")]
    [SerializeField] private TMPro.TextMeshProUGUI highScore;

    [Header("Cursor")]
    [SerializeField] private Sprite cursorSprite;
    [SerializeField] private Vector2 cursorHotspot = Vector2.zero;

    private Texture2D cursorTexture;

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

        // Set Cursor
        if (cursorSprite != null)
        {
            cursorTexture = new Texture2D((int)cursorSprite.textureRect.width, (int)cursorSprite.textureRect.height, TextureFormat.RGBA32, false);
            for (int y = 0; y < cursorTexture.height; y++)
            {
                for (int x = 0; x < cursorTexture.width; x++)
                {
                    cursorTexture.SetPixel(x, y, cursorSprite.texture.GetPixel((int)cursorSprite.textureRect.x + x, (int)cursorSprite.textureRect.y + y));
                }
            }
            cursorTexture.Apply();
        }

        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }
}
