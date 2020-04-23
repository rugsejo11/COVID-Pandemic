using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsManipulation : MonoBehaviour
{
    public bool toOpen = false;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

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
