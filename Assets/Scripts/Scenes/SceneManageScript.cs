using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManageScript : MonoBehaviour
{
    [SerializeField] private Animator transition = null; //Transition animator
    [SerializeField] private GameObject crossfade = null; // Crossfade object for switching between scenes
    private float transitionTime = 1f; // Transition time between scenes


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
        crossfade.SetActive(true);
        StartCoroutine(LoadIntro());
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

    /// <summary>
    /// Function to load next scene
    /// </summary>
    public static void LoadNextLevel(int currentSceneIndex)
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    /// <summary>
    /// Function to load intro scene
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadIntro()
    {
        transition.SetTrigger("StartGame");

        yield return new WaitForSeconds(transitionTime);

        LoadNextLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
