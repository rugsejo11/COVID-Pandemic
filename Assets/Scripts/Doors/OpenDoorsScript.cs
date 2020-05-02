using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorsScript : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    public bool complexLeverDone = false; public bool switcherDone = false; public bool clownLeverDone = false;
    public bool elevatorButtonDone = false; public bool hazardousLeverDone = false; public bool electricityLeverDone = false;
    public bool smallButtonDone = false; public bool finishLeverDone = false; public bool finishDetonator = false;
    private int currentStage = 1;
    
    void Start()
    {

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
                    break;
                case 2:
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
        if (finishLeverDone && finishDetonator && complexLeverDone)
            return true;
        else if (finishDetonator)
        {
            // Explode
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
            Debug.Log("Game finished");
            // end game
        }
    }
}
