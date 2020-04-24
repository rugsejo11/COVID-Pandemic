using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsManipulation : MonoBehaviour
{
    public bool toOpen = false;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (toOpen)
            OpenDoors();

    }

    public void OpenDoors()
    {
        //animator.SetBool("Open", true);
        animator.SetTrigger("Open");
    }
}
