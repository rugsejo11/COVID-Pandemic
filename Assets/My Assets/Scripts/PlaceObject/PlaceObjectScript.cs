using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectScript : MonoBehaviour
{
    #region Variables
    public Transform HeroHandsPosition; // Hands position of the character
    HeroInteractive hero; // Game character
    Rigidbody objectToTake; // Object that hero is trying to take

    // Rack slots
    public SocketScript Socket;  // First rack slot
    public SocketScript Socket2; // Second rack slot
    public SocketScript Socket3;
    public SocketScript Socket4;
    public SocketScript Socket5;
    public SocketScript Socket6;
    public SocketScript currentSocket; // In which slot object is placed
    int socketNum = int.MinValue; // number of the socket, where object is being placed


    // PossibleToGrabObject()
    float distance; // Distance between object and hero
    float angleView; // Angle view between object and hero
    Vector3 direction; // Direction between object and hero

    bool objectGrabbed = false; // Variable holding value if object is grabbed/being grabbed
    bool atSocket = false; // Variable holding value if object is positioned at a socket
    bool objectInHands = false; // Variable holding value if object is at hero's hands

    #endregion

    #region Functions
    /// <summary>
    /// Function triggered at the start of the script
    /// </summary>
    void Start()
    {
        currentSocket = GetComponent<SocketScript>(); // Get current socket
        hero = FindObjectOfType<HeroInteractive>(); // Get hero object
        objectToTake = GetComponent<Rigidbody>(); // Get object to take
    }

    /// <summary>
    /// Function triggered at every frame
    /// </summary>
    void Update()
    {
        GrabAnObject(); // Grab or drop object


        if (atSocket) // If object is at socket
            PlaceObject(); // If object colided with socket, position it at socket 
    }

    /// <summary>
    /// Function to place an object to a socket
    /// </summary>
    void PlaceObject()
    {
        switch (socketNum) // Switch to get in which socket to place an object
        {
            case 1:
                gameObject.transform.position = Socket.socket.transform.position; // Set grabbed object position to socket position
                gameObject.transform.rotation = Socket.socket.transform.rotation; // Set grabbed object rotation to socket rotation
                currentSocket.UseSocket(); // Mark socket as not empty
                break;
            case 2:
                gameObject.transform.position = Socket2.socket.transform.position;
                gameObject.transform.rotation = Socket2.socket.transform.rotation;
                currentSocket.UseSocket();
                break;
            case 3:
                gameObject.transform.position = Socket3.socket.transform.position;
                gameObject.transform.rotation = Socket3.socket.transform.rotation;
                currentSocket.UseSocket();
                break;
            case 4:
                gameObject.transform.position = Socket4.socket.transform.position;
                gameObject.transform.rotation = Socket4.socket.transform.rotation;
                currentSocket.UseSocket();
                break;
            case 5:
                gameObject.transform.position = Socket5.socket.transform.position;
                gameObject.transform.rotation = Socket5.socket.transform.rotation;
                currentSocket.UseSocket();
                break;
            case 6:
                gameObject.transform.position = Socket6.socket.transform.position;
                gameObject.transform.rotation = Socket6.socket.transform.rotation;
                currentSocket.UseSocket();
                break;
            default:
                Debug.LogError("Error, socket not found.");
                break;
        }
    }

    /// <summary>
    /// Function to grab or drop object
    /// </summary>
    void GrabAnObject()
    {
        if (PossibleToGrabObject() && Input.GetKeyDown(KeyCode.Mouse0) && !objectGrabbed && !hero.IsObjectGrabbed()) // If button pressed and object in range
        {

            if (currentSocket != null)
                currentSocket.EmptySocket(); // Empty socket if it's not empty

            atSocket = false; // Remove object from socket
            objectGrabbed = true; // Grab object
            hero.GrabObject(); // Set that hero has an object in he's hands
            objectInHands = false; // Set that object is not grabbed just yet
        }

        else if (objectGrabbed) // If object grabbed
        {
            if (Input.GetKeyUp(KeyCode.Mouse0)) // If Mouse 1 button realesed
            {
                objectGrabbed = false; // Drop object
                hero.DropObject(); // Free hero's hands
                objectToTake.drag = 0f;
                objectToTake.angularDrag = .5f;
            }
            else
            {
                if (!objectInHands) // If object dropped
                {
                    objectToTake.drag = 10f;
                    objectToTake.angularDrag = 10f;
                }

                else
                {
                    distance = Vector3.Distance(transform.position, Camera.main.transform.position);
                    if (distance > 3f) // If object too far, drop object
                    {
                        objectGrabbed = false;
                        hero.DropObject(); // Free hero's hands
                    }
                }

                objectInHands = true;
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, HeroHandsPosition.position, 1f); // Follow object in character's hands
            }
        }
    }

    /// <summary>
    /// Function checks if possible to grab object
    /// </summary>
    /// <returns></returns>
    bool PossibleToGrabObject()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position); // Distance between object and hero
        direction = transform.position - Camera.main.transform.position; // Direction between object and hero
        angleView = Vector3.Angle(Camera.main.transform.forward, direction); // Angle view between object and hero

        if (distance < 3f && angleView < 35f) // If distance and angle view is in range, return that it is possible to grab this object
            return true;
        else
            return false;
    }

    /// <summary>
    /// On collision between object and socket
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other == Socket.socket)
        {
            if (Socket.IsSocketEmpty()) // If socket which object has colided with is empty
            {
                atSocket = true; // Set object at socket
                socketNum = 1; // Socket number is 1
                currentSocket = Socket; // Object current socket is this socket
            }
        }
        else if (other == Socket2.socket)
        {
            if (Socket2.IsSocketEmpty())
            {
                atSocket = true;
                socketNum = 2;
                currentSocket = Socket2;
            }
        }
        else if (other == Socket3.socket)
        {
            if (Socket3.IsSocketEmpty())
            {
                atSocket = true;
                socketNum = 3;
                currentSocket = Socket3;
            }
        }
        else if (other == Socket4.socket)
        {
            if (Socket4.IsSocketEmpty())
            {
                atSocket = true;
                socketNum = 4;
                currentSocket = Socket4;
            }
        }
        else if (other == Socket5.socket)
        {
            if (Socket5.IsSocketEmpty())
            {
                atSocket = true;
                socketNum = 5;
                currentSocket = Socket5;
            }
        }
        else if (other == Socket6.socket)
        {
            if (Socket6.IsSocketEmpty())
            {
                atSocket = true;
                socketNum = 6;
                currentSocket = Socket6;
            }
        }
    }
    #endregion
}
