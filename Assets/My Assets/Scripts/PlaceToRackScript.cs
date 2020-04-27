using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceToRackScript : MonoBehaviour
{
    public Transform HeroHandsPosition;
    public Collider Socket; // need Trigger
    public Collider Socket2; // need Trigger
    int socketNum = int.MinValue;


    // NearView()
    float distance;
    float angleView;
    Vector3 direction;

    bool objectGrabbed = false;
    bool atSocket = false;
    bool followFlag = false;
    Rigidbody rb;
    bool firstTime = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Mouse0))
        //    Debug.Log("M1 pressed");
        Interaction();

        // frozen if it is connected to PowerOut
        if (atSocket)
        {
            switch (socketNum)
            {
                case 1:
                    gameObject.transform.position = Socket.transform.position;
                    gameObject.transform.rotation = Socket.transform.rotation;
                    break;
                case 2:
                    gameObject.transform.position = Socket2.transform.position;
                    gameObject.transform.rotation = Socket2.transform.rotation;
                    break;
                default:
                    Debug.LogError("Error, socket not found.");
                    break;
            }

        }
    }

    void Interaction()
    {
        if (NearView() && Input.GetKeyDown(KeyCode.Mouse0) && !objectGrabbed)
        {
            atSocket = false; // unfrozen
            objectGrabbed = true;
            followFlag = false;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            objectGrabbed = false;
            rb.drag = 0f;
            rb.angularDrag = .5f;
        }

        else if (objectGrabbed)
        {
            rb.drag = 10f;
            rb.angularDrag = 10f;
            if (followFlag)
            {
                distance = Vector3.Distance(transform.position, Camera.main.transform.position);
                if (distance > 3f)
                    objectGrabbed = false;
            }

            followFlag = true;
            //if (Vector3.Distance(gameObject.transform.position, HeroHandsPosition.position) > 1)
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, HeroHandsPosition.position, 1f);
        }

        else
        {
            rb.drag = 0f;
            rb.angularDrag = .5f;
        }
    }

    bool NearView() // it is true if you near interactive object
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        direction = transform.position - Camera.main.transform.position;
        angleView = Vector3.Angle(Camera.main.transform.forward, direction);
        if (distance < 3f && angleView < 35f) return true;
        else return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == Socket)
        {
            atSocket = true;
            socketNum = 1;
            //objectGrabbed = false;
        }
        if (other == Socket2)
        {
            atSocket = true;
            socketNum = 2;
            //objectGrabbed = false;
        }
    }
}
