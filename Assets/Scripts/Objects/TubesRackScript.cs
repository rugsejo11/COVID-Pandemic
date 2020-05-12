using UnityEngine;

public class TubesRackScript : MonoBehaviour
{
    #region Variables

    //Tube rack slots
    [SerializeField] private SocketScript socketScript = null;
    [SerializeField] private SocketScript socketScript2 = null;
    [SerializeField] private SocketScript socketScript3 = null;
    [SerializeField] private SocketScript socketScript4 = null;
    [SerializeField] private SocketScript socketScript5 = null;
    [SerializeField] private SocketScript socketScript6 = null;

    public TubesRack tubes { get; private set; } // Tubes rack

    #endregion


    #region Monobehaviour Functions
    /// <summary>
    /// Function initialize any variables or game state before the game starts
    /// </summary>
    void Awake()
    {
        tubes = new TubesRack();
        tubes.SetSocket(1, socketScript.socket);
        tubes.SetSocket(2, socketScript2.socket);
        tubes.SetSocket(3, socketScript3.socket);
        tubes.SetSocket(4, socketScript4.socket);
        tubes.SetSocket(5, socketScript5.socket);
        tubes.SetSocket(6, socketScript6.socket);
    }

    #endregion
}
public class TubesRack
{
    #region Variables

    private Socket socket;
    private Socket socket2;
    private Socket socket3;
    private Socket socket4;
    private Socket socket5;
    private Socket socket6;

    #endregion

    #region Functions Get, Set And Count Used Sockets

    /// <summary>
    /// Set sockets for testing
    /// </summary>
    /// <param name="socketNumber"></param>
    /// <param name="socket"></param>
    public void SetSocket(int socketNumber, Socket s)
    {
        switch (socketNumber)
        {
            case 1:
                socket = s;
                break;
            case 2:
                socket2 = s;
                break;
            case 3:
                socket3 = s;
                break;
            case 4:
                socket4 = s;
                break;
            case 5:
                socket5 = s;
                break;
            case 6:
                socket6 = s;
                break;
            default:
                Debug.LogError("Wrong socket number entered!");
                break;
        }
    }

    /// <summary>
    /// Get test rack socket
    /// </summary>
    /// <param name="socketNumber">socket number</param>
    /// <returns></returns>
    public Socket GetSocket(int socketNumber)
    {
        switch (socketNumber)
        {
            case 1:
                return socket;
            case 2:
                return socket2;
            case 3:
                return socket3;
            case 4:
                return socket4;
            case 5:
                return socket5;
            case 6:
                return socket6;
            default:
                Debug.LogError("Wrong socket number entered!");
                return socket;
        }
    }

    /// <summary>
    /// Get number of used sockets in the test rack
    /// </summary>
    /// <returns></returns>
    public int UsedSockets()
    {
        int usedSockets = 0;

        for (int i = 1; i <= 6; i++)
        {
            if (!GetSocket(i).IsSocketEmpty())
                usedSockets++;
        }
        return usedSockets;
    }

    #endregion
}
