using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorsScript : MonoBehaviour
{

    public bool firstDoorsOpen = false; 
    public bool secondDoorsOpen = false;
    public Animator animator;

    public void OpenDoors(int doorsNumber)
    {
        if (animator != null)
        {
            switch (doorsNumber)
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





    public bool complexLeverDone = false; public bool switcherDone = false; public bool clownLeverDone = false;
    public bool elevatorButtonDone = false; public bool hazardousLeverDone = false; public bool electricityLeverDone = false;
    public bool smallButtonDone = false; public bool finishLeverDone = false; public bool finishDetonator = false;

    private int currentStage = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    bool FirstStage()
    {
        string answer = string.Format("{0} {1} {2}", smallButtonDone, hazardousLeverDone, switcherDone);
        Debug.Log(answer);
        if (smallButtonDone && hazardousLeverDone && switcherDone)
            return true;
        return false;
    }
    bool SecondStage()
    {
        if (clownLeverDone && electricityLeverDone && elevatorButtonDone)
            return true;
        return false;
    }
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
