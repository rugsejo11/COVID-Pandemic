using UnityEngine;

public class PlaceToRackScript
{

    /// <summary>
    /// Function to place an object to a socket
    /// </summary>
    public Vector3 SetTubePosition(GameObject gameObject, Vector3 position)
    {
        gameObject.transform.position = position; // Set grabbed object position to socket position

        return gameObject.transform.position;
    }

}