using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsManipulation : MonoBehaviour
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

}
