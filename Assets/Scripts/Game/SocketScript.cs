using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketScript : MonoBehaviour
{

    [SerializeField] private bool canBeUsed = true; // Variable holding value if socket can be used
    [SerializeField] private bool socketEmpty = true; // Variable holding value if socket is empty
    [SerializeField] private GameObject testTube = null;
    [SerializeField] private GameObject desiredTestTube = null;
    private HeroInteractive hero; // Game character

    void Start()
    {
        hero = FindObjectOfType<HeroInteractive>(); // Get hero object
    }

    /// <summary>
    /// Function to set use socket
    /// </summary>
    public void UseSocket()
    {
        if (canBeUsed)
            socketEmpty = false; // use socket
    }

    /// <summary>
    /// Function to set socket empty
    /// </summary>
    public void EmptySocket()
    {
        socketEmpty = true; // empty socket 
        this.tag = "Socket";
    }

    /// <summary>
    /// Function to check if socket is used or empty
    /// </summary>
    /// <returns></returns>
    public bool IsSocketEmpty()
    {
        return socketEmpty; // return state of socket (empty or used)
    }

    /// <summary>
    /// Function to check if attached testTube is the one that has to be attached
    /// </summary>
    /// <returns></returns>
    public bool isDesired()
    {
        return testTube == desiredTestTube;
    }

    /// <summary>
    /// Function when the Collider other collides with socket.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TestTube")
        {
            UseSocket();
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;
            testTube = other.gameObject;
            if (!isDesired())
            {
                hero.LoseHP();
            }
        }
    }

    /// <summary>
    /// Function when the Collider other has stopped touching the trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        EmptySocket();
        testTube = null;
    }
}
