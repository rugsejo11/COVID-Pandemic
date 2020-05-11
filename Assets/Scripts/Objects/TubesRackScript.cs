using UnityEngine;

public class TubesRackScript : MonoBehaviour
{
    #region Variables

    //Tube rack slots
    [SerializeField] private Socket Socket = null;
    [SerializeField] private Socket Socket2 = null;
    [SerializeField] private Socket Socket3 = null;
    [SerializeField] private Socket Socket4 = null;
    [SerializeField] private Socket Socket5 = null;
    [SerializeField] private Socket Socket6 = null;

    private TubesRack tubes; // Tubes rack

    #endregion


    #region Monobehaviour Functions
    /// <summary>
    /// Function initialize any variables or game state before the game starts
    /// </summary>
    void Awake()
    {
        tubes = new TubesRack();
        tubes.SetSocket(1, Socket);
        tubes.SetSocket(2, Socket2);
        tubes.SetSocket(3, Socket3);
        tubes.SetSocket(4, Socket4);
        tubes.SetSocket(5, Socket5);
        tubes.SetSocket(6, Socket6);
    }

    #endregion
}
public class TubesRack
{
    #region Variables

    private Socket Socket;
    private Socket Socket2;
    private Socket Socket3;
    private Socket Socket4;
    private Socket Socket5;
    private Socket Socket6;

    #endregion

    #region Functions Get, Set And Count Used Sockets

    /// <summary>
    /// Set sockets for testing
    /// </summary>
    /// <param name="socketNumber"></param>
    /// <param name="socket"></param>
    public void SetSocket(int socketNumber, Socket socket)
    {
        switch (socketNumber)
        {
            case 1:
                Socket = socket;
                break;
            case 2:
                Socket2 = socket;
                break;
            case 3:
                Socket3 = Socket;
                break;
            case 4:
                Socket4 = socket;
                break;
            case 5:
                Socket5 = socket;
                break;
            case 6:
                Socket6 = socket;
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
                return Socket;
            case 2:
                return Socket2;
            case 3:
                return Socket3;
            case 4:
                return Socket4;
            case 5:
                return Socket5;
            case 6:
                return Socket6;
            default:
                Debug.LogError("Wrong socket number entered!");
                return Socket;
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
