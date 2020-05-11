using UnityEngine;

public class ObjectDistanceScript
{
    #region Variables

    private float distance; // Variable holding distance between hero and item
    private float angleView; // Variable holding angle difference between hero camera and item
    private Vector3 direction; // Varialbe holding hero camera direction

    #endregion

    #region Functions

    /// <summary>
    /// Function to calculate distance, direction and angleview values and check if switch is reachable
    /// </summary>
    /// <returns></returns>
    public bool IsObjectInRange(Vector3 position, Transform transform)
    {
        distance = GetDistance(position, transform.position);
        direction = GetDirection(position, transform.position);
        angleView = GetAngleView(transform.forward, direction);

        if (CheckDistanceAndAngle(distance, angleView))
            return true;
       return false;

    }

    /// <summary>
    /// Function to check if distance and angleview values are up to limits
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="angleView"></param>
    /// <returns></returns>
    public bool CheckDistanceAndAngle(float distance, float angleView)
    {
        if (distance > 0f && distance < 2f && angleView > 0f && angleView < 60f) // If distance and angle view is in range, return that it is possible to grab this object
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Get distance value between hero and object
    /// </summary>
    /// <param name="position"></param>
    /// <param name="position2"></param>
    /// <returns></returns>
    public float GetDistance(Vector3 position, Vector3 position2)
    {
        distance = Vector3.Distance(position, position2);

        return distance;
    }

    /// <summary>
    /// Get direction value between hero and object
    /// </summary>
    /// <param name="position"></param>
    /// <param name="position2"></param>
    /// <returns></returns>
    public Vector3 GetDirection(Vector3 position, Vector3 position2)
    {
        direction = position - position2;

        return direction;

    }

    /// <summary>
    /// Get Angle view value between hero and object
    /// </summary>
    /// <param name="forward"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public float GetAngleView(Vector3 forward, Vector3 direction)
    {
        Vector3 newDirection = new Vector3();

        newDirection.Set(direction.x * 10, direction.y, direction.z);

        angleView = Vector3.Angle(forward, newDirection);

        return angleView;
    }

    #endregion
}
