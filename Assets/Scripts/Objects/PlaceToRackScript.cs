using UnityEngine;

public class PlaceToRackScript : MonoBehaviour
{
    #region Variables
    // Hero
    [SerializeField] private Transform HeroHandsPosition = null; // Hands position of the character
    [SerializeField] private HeroDataScript hero; // Game character
    [SerializeField] private Rigidbody characterBody = null; // Character body
    private Rigidbody objectToTake; // Object that hero is trying to take

    // Test tube rack slot
    [SerializeField] private GameObject currentSocket = null; // In which slot object is placed

    // IsObjectInRange()
    private float distance; // Variable holding distance between hero and item
    private float angleView; // Variable holding angle difference between hero camera and item
    private Vector3 direction; // Variable holding hero camera direction
    
    // Hero and tube data
    private bool objectGrabbed = false; // Variable holding value if object is grabbed/being grabbed
    private bool objectInHands = false; // Variable holding value if object is at hero's hands
    private bool atSocket = false; // Variable holding value if object is positioned at a socket

    //Time after removing from rack
    private float timeAfterRemovingFromRack;

    #endregion

    #region Functions
    /// <summary>
    /// Function triggered at the start of the script
    /// </summary>
    void Start()
    {
        hero = FindObjectOfType<HeroDataScript>(); // Get hero object
        objectToTake = GetComponent<Rigidbody>(); // Get object to take
        timeAfterRemovingFromRack = 2f;
    }

    /// <summary>
    /// Function triggered at every frame
    /// </summary>
    void Update()
    {
        GrabAnObject(); // Grab or drop object

        if (atSocket) // If object is at socket
        {
            PlaceToRack(); // If object colided with socket, position it at socket 
        }
        else
        {
            timeAfterRemovingFromRack += Time.deltaTime;
        }
    }

    /// <summary>
    /// Function to grab or drop object
    /// </summary>
    void GrabAnObject()
    {
        if (IsObjectInRange() && Input.GetKeyDown(KeyCode.Mouse0) && !objectGrabbed && !hero.IsObjectGrabbed()) // If button pressed and object in range
        {

            if (currentSocket != null)
            {
                currentSocket.gameObject.tag = "Socket";
                currentSocket = null; // Empty socket if it's not empty
            }

            atSocket = false; // Remove object from socket
            timeAfterRemovingFromRack = 0;
            objectGrabbed = true; // Grab object
            hero.GrabObject(); // Set that hero has an object in he's hands
            objectInHands = false; // Set that object is not grabbed just yet
        }

        else if (objectGrabbed) // If object grabbed
        {
            if (Input.GetKeyUp(KeyCode.Mouse0)) // If Mouse 1 button realesed
            {
                objectGrabbed = false; // Drop object
                hero.DropObject(); // Free hero's hands
                objectToTake.drag = 0f;
                objectToTake.angularDrag = .5f;
            }
            else
            {
                if (!objectInHands) // If object not in hands just yet
                {
                    objectToTake.drag = 10f;
                    objectToTake.angularDrag = 10f;
                    objectToTake.transform.rotation = Quaternion.Euler(0, 0, 0);
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

                objectInHands = true;
                objectToTake.transform.rotation = Quaternion.Euler(0, 0, 0);
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, HeroHandsPosition.position, 1f); // Follow object in character's hands
            }
        }
    }

    /// <summary>
    /// Function to check if possible to grab object
    /// </summary>
    /// <returns></returns>
    bool IsObjectInRange()
    {
        Vector3 newDirection = new Vector3();

        distance = Vector3.Distance(transform.position, Camera.main.transform.position); // Distance between object and hero
        direction = transform.position - Camera.main.transform.position; // Direction between object and hero

        newDirection.Set(direction.x * 10, direction.y, direction.z);

        angleView = Vector3.Angle(Camera.main.transform.forward, newDirection); // Angle view between object and hero

        if (distance < 2f && angleView < 60f) // If distance and angle view is in range, return that it is possible to grab this object
        {
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// On collision between object and socket
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Socket" && timeAfterRemovingFromRack >= 2)
        {
            atSocket = true;
            currentSocket = other.gameObject;
            other.gameObject.tag = "UsedSocket";
        }
    }
    /// <summary>
    /// On collission with the room, push character back, that he would not push item through wall
    /// </summary>
    /// <param name="collision"></param>
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

    /// <summary>
    /// Function to place an object to a socket
    /// </summary>
    void PlaceToRack()
    {
        gameObject.transform.position = currentSocket.transform.position; // Set grabbed object position to socket position
        gameObject.transform.rotation = currentSocket.transform.rotation; // Set grabbed object rotation to socket rotation
    }
    #endregion
}
