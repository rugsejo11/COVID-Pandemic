using UnityEngine;

public class SocketScript : MonoBehaviour
{
    #region Variables

    [SerializeField] private bool canBeUsed = true; // Variable holding value if socket can be used
    [SerializeField] private bool socketEmpty = true; // Variable holding value if socket is empty
    [SerializeField] private GameObject testTube = null; // Variable holding current attached test tube
    [SerializeField] private GameObject desiredTestTube = null; // Variable holding desired test tube
    private HeroDataScript hero; // Game character
    private Socket socket; // Initialize Socket class

    #endregion

    #region Monobehaviour Functions

    /// <summary>
    /// Function initialize any variables or game state before the game starts
    /// </summary>
    void Awake()
    {
        hero = FindObjectOfType<HeroDataScript>(); // Get hero object
        socket = new Socket();
        socket.canBeUsed = canBeUsed;
        socket.socketEmpty = socketEmpty;
        socket.testTube = testTube;
        socket.desiredTestTube = desiredTestTube;
        socket.hero = hero;
    }

    /// <summary>
    /// Function is called every frame
    /// </summary>
    void Update()
    {
        socket.testTube = testTube;
        socket.socketEmpty = socketEmpty;
    }

    /// <summary>
    /// Function when the Collider other collides with the Socket
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TestTube")
        {
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;
            testTube = other.gameObject;
            socketEmpty = false;

            if (desiredTestTube != other.gameObject && hero.GetTimeAfterTakingFromRack() >= 2)
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
        tag = "Socket";
        socketEmpty = true; // empty socket 
        testTube = null;
    }

    #endregion
}
public class Socket
{
    #region Get Set Functions

    public bool canBeUsed { get; set; }
    public bool socketEmpty { get; set; } = true;
    public GameObject testTube { get; set; }
    public GameObject desiredTestTube { get; set; }
    public HeroDataScript hero { get; set; }

    #endregion

    #region Functions To Manipulate Socket

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

    #endregion

    #region Functions For Testing

    /// <summary>
    /// Insert test tube to test rack
    /// </summary>
    /// <param name="insertedTube">test tube to insert</param>
    public void TestTubeInserted(GameObject insertedTube)
    {
        testTube = insertedTube;
        socketEmpty = false;
    }

    /// <summary>
    /// Get desired test tube
    /// </summary>
    /// <returns></returns>
    public GameObject GetDesired()
    {
        return desiredTestTube;
    }

    /// <summary>
    /// Set desired test tube
    /// </summary>
    /// <param name="desiredObject">desired object</param>
    public void SetDesired(GameObject desiredObject)
    {
        desiredTestTube = desiredObject;
    }

    #endregion
}