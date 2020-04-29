using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabProps : MonoBehaviour
{
    #region Variables
    public bool OneTime = false; // If Possible To Grab Only One Time
    public Transform HeroHandsPosition; // Hands position of the character
    public Collider Destination; // Destination point where object can be attached

    // Is object nearview
    float distance; // Distance from char to item
    float angleView; // Angle difference from char camera to item
    Vector3 direction; // Character camera direction

    bool objectGrabbed = false; // Item grabbed
    bool atDestination = false; // Item is attached to it's destination
    bool possibleToGrab = true; // Is it possible to grab this item

    Rigidbody objectToTake;
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        objectToTake = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (possibleToGrab) Interaction();

        // frozen if it is connected to PowerOut
        if (atDestination)
        {
            gameObject.transform.position = Destination.transform.position;
            gameObject.transform.rotation = Destination.transform.rotation;
            //OBJECT SET ON
        }
    }
    void Interaction()
    {
        //if (NearView() && Input.GetKeyDown(KeyCode.E) && !follow)
        if (NearView() && Input.GetKeyDown(KeyCode.Mouse0) && !objectGrabbed)
        {
            atDestination = false; // unfrozen
            objectGrabbed = true;
        }

        if (objectGrabbed)
        {
            objectToTake.drag = 10f;
            objectToTake.angularDrag = 10f;
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            if (distance > 3f || Input.GetKeyDown(KeyCode.Mouse0))
            {
                objectGrabbed = false;
            }
            //objectToTake.AddExplosionForce(-1000f, HeroHandsPosition.position, 10f);
            // second variant of following
            //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, objectLerp.position, 1f);
        }
        else
        {
            objectToTake.drag = 0f;
            objectToTake.angularDrag = .5f;
        }
    }

    bool NearView() // it is true if you near interactive object
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        direction = transform.position - Camera.main.transform.position;
        angleView = Vector3.Angle(Camera.main.transform.forward, direction);
        if (distance < 2f && angleView < 10f) return true;
        else return false;
    }

    private void OnTriggerEnter(Collider objectTaken)
    {
        if (objectTaken == Destination)
        {
            atDestination = true;
            objectGrabbed = false;
            //DoorObject.rbDoor.AddRelativeTorque(new Vector3(0, 0, 20f));
        }
        if (OneTime) possibleToGrab = false;
    }
    #endregion
}
