using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDistanceScript
{
    public Vector3 position { get; set; }
    public Transform transform { get; set; }

    private float distance; // Variable holding distance between hero and item
    private float angleView; // Variable holding angle difference between hero camera and item
    private Vector3 direction; // Varialbe holding hero camera direction

    /// <summary>
    /// Function to check if switch is reachable
    /// </summary>
    /// <returns></returns>
    public bool IsObjectInRange(Vector3 position, Transform transform)
    {
        distance = GetDistance(position, transform.position);
        direction = GetDirection(position, transform.position);
        angleView = GetAngleView(transform.forward, direction);

        if (distance > 0f && distance < 2f && angleView > 0f && angleView < 60f) // If distance and angle view is in range, return that it is possible to grab this object
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    public float GetDistance(Vector3 position, Vector3 position2)
    {
        distance = Vector3.Distance(position, position2);

        return distance;
    }

    public Vector3 GetDirection(Vector3 position, Vector3 position2)
    {
        direction = position - position2;

        return direction;

    }

    public float GetAngleView(Vector3 forward, Vector3 direction)
    {
        Vector3 newDirection = new Vector3();

        newDirection.Set(direction.x * 10, direction.y, direction.z);


        angleView = Vector3.Angle(forward, newDirection);

        return angleView;
    }
}
