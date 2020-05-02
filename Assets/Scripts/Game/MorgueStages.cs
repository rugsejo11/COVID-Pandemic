using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorgueStages : MonoBehaviour
{
    //[SerializeField] private OpenDoorsScript dm = null;


    //private bool complexLeverDone = false; private bool switcherDone = false; private bool clownLeverDone = false;
    //private bool elevatorButtonDone = false; private bool hazardousLeverDone = false; private bool electricityLeverDone = false;
    //private bool smallButtonDone = false; private bool finishLeverDone = false; private bool finishDetonator = false;

    //private int currentStage = 1;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    dm = FindObjectOfType<OpenDoorsScript>(); // Get current socket
    //}

    //bool FirstStage()
    //{
    //    string answer = string.Format("{0} {1} {2}", smallButtonDone, hazardousLeverDone, switcherDone);
    //    Debug.Log(answer);
    //    if (smallButtonDone && hazardousLeverDone && switcherDone)
    //        return true;
    //    return false;
    //}
    //bool SecondStage()
    //{
    //    if (clownLeverDone && electricityLeverDone && elevatorButtonDone)
    //        return true;
    //    return false;
    //}
    //bool LastStage()
    //{
    //    if (finishLeverDone && finishDetonator && complexLeverDone)
    //        return true;
    //    else if (finishDetonator)
    //    {
    //        // Explode
    //    }
    //    return false;
    //}
    ///// <summary>
    ///// Function that check if stage is finished
    ///// </summary>
    //public void CheckIfStageFinished()
    //{
    //    if (currentStage == 1 && FirstStage())
    //    {
    //        if (dm != null)
    //        {
    //            dm.OpenDoors(currentStage);
    //            currentStage = 2;
    //        }
    //    }
    //    else if (currentStage == 2 && FirstStage() && SecondStage())
    //    {
    //        if (dm != null)
    //        {
    //            dm.OpenDoors(currentStage);
    //            currentStage = 3;
    //        }
    //    }
    //    else if (currentStage == 3 && FirstStage() && SecondStage() && LastStage())
    //    {
    //        Debug.Log("Game finished");
    //        // end game
    //    }
    //}
}
