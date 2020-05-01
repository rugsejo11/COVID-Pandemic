using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabProps : MonoBehaviour
{
    #region Variables
    private HeroInteractive hero; // Game character
    [SerializeField] private Transform HeroHandsPosition = null; // Hands position of the character

    // Is object PossibleToGrabObject
    private float distance; // Distance from char to item
    private float angleView; // Angle difference from char camera to item
    private Vector3 direction; // Character camera direction

    private bool objectGrabbed = false; // Item grabbed
    [SerializeField] private bool possibleToGrab = true; // Is it possible to grab this item
    private bool objectInHands = false; // Variable holding value if object is at hero's hands
    [SerializeField] private Rigidbody characterBody = null; // Character body


    Rigidbody objectToTake;
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        hero = FindObjectOfType<HeroInteractive>(); // Get hero object
        objectToTake = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (possibleToGrab) Interaction();
    }
    void Interaction()
    {
        if (PossibleToGrabObject() && Input.GetKeyDown(KeyCode.Mouse0) && !objectGrabbed && !hero.IsObjectGrabbed())
        {
            objectGrabbed = true;
            hero.GrabObject(); // Set that hero has an object in he's hands
            objectInHands = false; // Set that object is not grabbed just yet
        }

        else if (objectGrabbed)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0)) // If Mouse 1 button realesed
            {
                objectGrabbed = false;
                hero.DropObject(); // Free hero's hands
                objectToTake.drag = 0f;
                objectToTake.angularDrag = .5f;
            }

            else
            {
                if (!objectInHands) // If object dropped
                {
                    objectToTake.drag = 10f;
                    objectToTake.angularDrag = 10f;
                }
                else
                {
                    distance = Vector3.Distance(transform.position, Camera.main.transform.position);
                    if (distance > 3f) // If object too far, drop object
                    {
                        objectGrabbed = false;
                        hero.DropObject(); // Free hero's hands
                    }
                }
            }
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            if (distance > 3f || Input.GetKeyDown(KeyCode.Mouse0))
            {
                objectGrabbed = false;
            }

            objectInHands = true;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, HeroHandsPosition.position, 1f); // Follow object in character's hands
        }
    }

    bool PossibleToGrabObject() // it is true if you near interactive object
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position); // Distance between object and hero
        direction = transform.position - Camera.main.transform.position; // Direction between object and hero
        angleView = Vector3.Angle(Camera.main.transform.forward, direction); // Angle view between object and hero

        if (distance < 3f && angleView < 20f) // If distance and angle view is in range, return that it is possible to grab this object
            return true;
        else
            return false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Room") && objectGrabbed)
        {
            float force = 1500;
            Vector3 dir = collision.contacts[0].point - transform.position;
            dir = -dir.normalized;
            characterBody.AddForce(dir * force);
        }
    }
    #endregion
}
