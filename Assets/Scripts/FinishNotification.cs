using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishNotification : MonoBehaviour
{
    private float distance; // Distance between object and hero
    private float angleView; // Angle view between object and hero
    private Vector3 direction; // Direction between object and hero
    [SerializeField] private GameObject finishNotification = null;
    bool DetonatorNotPressed = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Interaction();
    }

    void Interaction()
    {
        if (PossibleToGrabObject() && DetonatorNotPressed)
        {
            ShowNotification(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                DetonatorNotPressed = false;
                ShowNotification(false);
            }
        }
    }
    bool PossibleToGrabObject()
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
            ShowNotification(false);
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enabled"></param>
    void ShowNotification(bool enabled)
    {
        if (enabled)
        {
            finishNotification.SetActive(true);
        }
        else
            finishNotification.SetActive(false);
    }
}
