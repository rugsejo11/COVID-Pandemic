using UnityEngine;

public class FinishNotificationScript : MonoBehaviour
{
    private float distance; // Distance between object and hero
    private float angleView; // Angle view between object and hero
    private Vector3 direction; // Direction between object and hero
    [SerializeField] private GameObject finishNotification = null; // Variable holding finish notification game object
    private bool DetonatorNotPressed = true; // Variable holding value if detonator has been pressed


    /// <summary>
    /// Function is called every frame
    /// </summary>
    void Update()
    {
        ShowFinishNotification();
    }

    /// <summary>
    /// Function to show finish game notification
    /// </summary>
    private void ShowFinishNotification()
    {
        if (IsObjectCloseEnough() && DetonatorNotPressed)
        {
            EnableNotification(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                DetonatorNotPressed = false;
                EnableNotification(false);
            }
        }
    }

    /// <summary>
    /// Function to check if object is close enough to interact
    /// </summary>
    /// <returns></returns>
    private bool IsObjectCloseEnough()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position); // Distance between object and hero
        direction = transform.position - Camera.main.transform.position; // Direction between object and hero
        angleView = Vector3.Angle(Camera.main.transform.forward, direction); // Angle view between object and hero

        if (distance < 2.5f && angleView < 45f) // If distance and angle view is in range, return that it is possible to grab this object
        {
            return true;
        }
        else
        {
            EnableNotification(false);
            return false;
        }
    }

    /// <summary>
    /// Function to enable or disable finish notification
    /// </summary>
    /// <param name="enabled"></param>
    private void EnableNotification(bool enabled)
    {
        if (enabled)
        {
            finishNotification.SetActive(true);
        }
        else
            finishNotification.SetActive(false);
    }
}
