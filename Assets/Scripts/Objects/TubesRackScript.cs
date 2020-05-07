using UnityEngine;

public class TubesRackScript : MonoBehaviour
{
    //Tube rack slots
    [SerializeField] private SocketScript Socket = null;
    [SerializeField] private SocketScript Socket2 = null;
    [SerializeField] private SocketScript Socket3 = null;
    [SerializeField] private SocketScript Socket4 = null;
    [SerializeField] private SocketScript Socket5 = null;
    [SerializeField] private SocketScript Socket6 = null;

    /// <summary>
    /// Get test rack socket
    /// </summary>
    /// <param name="socketNumber">socket number</param>
    /// <returns></returns>
    public SocketScript GetSocket(int socketNumber)
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
}
