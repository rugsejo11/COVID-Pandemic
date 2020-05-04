using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageLevel : MonoBehaviour
{
    #region Variables

    [SerializeField] private Animator animator = null;

    public bool complexLeverDone = false; public bool switcherDone = false; public bool clownLeverDone = false;
    public bool elevatorButtonDone = false; public bool hazardousLeverDone = false; public bool electricityLeverDone = false;
    public bool smallButtonDone = false; public bool finishLeverDone = false; public bool finishDetonator = false;
    private HeroInteractive hero; // Game character

    private int currentStage = 1;

    #endregion
    void Start()
    {
        hero = FindObjectOfType<HeroInteractive>(); // Get hero object
    }

    /// <summary>
    /// Function to open doors because stage is finished
    /// </summary>
    /// <param name="doorsToOpen"></param>
    public void OpenDoors(int doorsToOpen)
    {
        if (animator != null)
        {
            switch (doorsToOpen)
            {
                case 1:
                    animator.SetBool("FirstDoorsOpen", true);
                    FindObjectOfType<AudioManager>().Play("first_doors_open"); // Play Button Press Audio
                    break;
                case 2:
                    FindObjectOfType<AudioManager>().Play("second_doors_open"); // Play Button Press Audio
                    animator.SetBool("SecondDoorsOpen", true);
                    break;
                default:
                    Debug.LogError("Doors not found.");
                    break;
            }
        }
    }

    /// <summary>
    /// Function to check if first stage is finished
    /// </summary>
    /// <returns></returns>
    bool FirstStage()
    {
        if (smallButtonDone && hazardousLeverDone && switcherDone)
            return true;
        return false;
    }

    /// <summary>
    /// Function to check if second stage is finished
    /// </summary>
    /// <returns></returns>
    bool SecondStage()
    {
        if (clownLeverDone && electricityLeverDone && elevatorButtonDone)
            return true;
        return false;
    }

    /// <summary>
    /// Function to check if third stage is finished
    /// </summary>
    /// <returns></returns>
    bool LastStage()
    {
        if (finishLeverDone && finishDetonator && complexLeverDone && hero.WereHandsWashed())
        {
            return true;
        }
        else if (finishDetonator)
        {
            FindObjectOfType<AudioManager>().Play("explosion"); // Play Button Press Audio
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        return false;
    }

    /// <summary>
    /// Function that check if stage is finished
    /// </summary>
    public void CheckIfStageFinished()
    {
        if (currentStage == 1 && FirstStage())
        {
                OpenDoors(currentStage);
                currentStage = 2;
        }
        else if (currentStage == 2 && FirstStage() && SecondStage())
        {
                OpenDoors(currentStage);
                currentStage = 3;
        }
        else if (currentStage == 3 &&  FirstStage() && SecondStage() && LastStage())
        {
            FindObjectOfType<AudioManager>().Play("level_finished"); // Play Button Press Audio
            Debug.Log("Level finished");
        }
    }
}
