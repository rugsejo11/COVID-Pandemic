using UnityEngine;

public class SocketScript : MonoBehaviour
{

    [SerializeField] private bool canBeUsed = true; // Variable holding value if socket can be used
    [SerializeField] private bool socketEmpty = true; // Variable holding value if socket is empty
    [SerializeField] private GameObject testTube = null; // Variable holding current attached test tube
    [SerializeField] private GameObject desiredTestTube = null; // Variable holding desired test tube
    private HeroDataScript hero; // Game character

    /// <summary>
    /// Function is called on the frame when a script is enabled just before any of the Update methods are called the first time
    /// </summary>
    void Start()
    {
        hero = FindObjectOfType<HeroDataScript>(); // Get hero object
    }

    /// <summary>
    /// Function to use socket
    /// </summary>
    public void UseSocket()
    {
        if (canBeUsed)
        {
            socketEmpty = false; // use socket
        }
    }

    /// <summary>
    /// Function to set socket empty
    /// </summary>
    public void EmptySocket()
    {
        socketEmpty = true; // empty socket 
        tag = "Socket";
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
    /// Function when the Collider other collides with socket
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
