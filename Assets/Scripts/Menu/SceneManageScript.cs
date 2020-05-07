using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManageScript : MonoBehaviour
{
    /// <summary>
    /// Function to load menu scene
    /// </summary>
    public static void GetToMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Function to start the game
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Function to load second level
    /// </summary>
    public void SecondLevel()
    {
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// Function to
    /// </summary>
    public void PlayDemo()
    {
        SceneManager.LoadScene(3);

    }

    /// <summary>
    /// Function to quit game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Function to restart active scene
    /// </summary>
    public static void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
