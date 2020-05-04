using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketScript : MonoBehaviour
{

    public bool canBeUsed = true; // Variable holding value if socket can be used
    public bool socketEmpty = true; // Variable holding value if socket is empty
    public SphereCollider socket; // Variable holding socket collider
    public GameObject tube = null;

    /// <summary>
    /// Function triggered at the start of the function
    /// </summary>
    void Start()
    {
        socket = GetComponent<SphereCollider>(); // get socket sphere collider
    }

    /// <summary>
    /// Function to set socket as used
    /// </summary>
    public void UseSocket()
    {
        socketEmpty = false; // use socket
    }

    /// <summary>
    /// Function to set socket as not used
    /// </summary>
    public void EmptySocket()
    {
        socketEmpty = true; // empty socket 
    }

    /// <summary>
    /// Function to check if socket is used
    /// </summary>
    /// <returns></returns>
    public bool IsSocketEmpty()
    {
        return socketEmpty; // return state of socket (empty or used)
    }

    /// <summary>
    /// On collision between object and socket
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TestTube")
        {
            socketEmpty = false;
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;
            tube = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        socketEmpty = true;
        tube = null;
    }
}
