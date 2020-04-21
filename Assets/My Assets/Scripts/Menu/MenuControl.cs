using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    /// <summary>
    /// When Play Button Pressed Game Scene Loaded
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// When ESC Button Pressed Menu Scene Loaded
    /// </summary>
    public static void GetToMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// When Quit Button Pressed Exit From Game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowHighscores()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayDemo()
    {
        SceneManager.LoadScene(3);
    }
}
