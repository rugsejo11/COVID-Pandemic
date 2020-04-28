using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInteractive : MonoBehaviour
{
    public Transform GoalPosition; // Level goal position
    public bool objectGrabbed = false; // Variable holding value if hero has object in he's hands

    /// <summary>
    /// Function to fill hero's hands 
    /// </summary>
    public void GrabObject()
    {
        objectGrabbed = true;

    }

    /// <summary>
    /// Function to empty hero's hands
    /// </summary>
    public void DropObject()
    {
        objectGrabbed = false;
    }

    /// <summary>
    /// Function to check if hero's hands are empty
    /// </summary>
    /// <returns></returns>
    public bool IsObjectGrabbed()
    {
        return objectGrabbed;
    }
}
